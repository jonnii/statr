$(document).ready(function () {
    var App = Em.Application.create();

    App.store = DS.Store.create({
        revision: 4,
        adapter: DS.RESTAdapter.create({
            bulkCommit: false,
            namespace: "api"
        })
    });

    App.ApplicationController = Em.Controller.extend();
    App.ApplicationView = Em.View.extend({
        templateName: 'application'
    });

    App.NavigationController = Em.Controller.extend();
    App.NavigationView = Em.View.extend({
        templateName: 'navigation'
    });

    App.FooterView = Em.View.extend({
        templateName: 'footer'
    });

    App.HomeController = Em.Controller.extend();
    App.HomeView = Em.View.extend({
        templateName: 'home'
    });

    App.ConfigurationController = Em.Controller.extend();
    App.ConfigurationView = Em.View.extend({
        templateName: 'configuration'
    });

    App.BucketsController = Em.ArrayController.extend();
    App.BucketsView = Em.View.extend({
        templateName: 'buckets'
    });
    App.Bucket = DS.Model.extend({
        
    });

    App.Router = Em.Router.extend({
        enableLogging: true,

        goHome: Ember.Route.transitionTo('home'),
        goConfiguration: Ember.Route.transitionTo('configuration'),
        goBuckets: Ember.Route.transitionTo('buckets'),

        root: Em.Route.extend({
            index: Em.Route.extend({
                route: '/',
                redirectsTo: "home"
            }),

            home: Em.Route.extend({
                route: '/home',
                connectOutlets: function (router, context) {
                    router.get('applicationController').connectOutlet({
                        name: 'home'
                    });
                },
                enter: function () {
                    $('ul.nav li').removeClass('active');
                    $('#nav-home').addClass('active');
                }
            }),

            configuration: Em.Route.extend({
                route: '/configuration',
                connectOutlets: function (router, context) {
                    router.get('applicationController').connectOutlet({
                        name: 'configuration'
                    });
                },
                enter: function () {
                    $('ul.nav li').removeClass('active');
                    $('#nav-configuration').addClass('active');
                }
            }),

            buckets: Em.Route.extend({
                route: '/buckets',
                connectOutlets: function (router, context) {
                    router.get('applicationController').connectOutlet({
                        name: 'buckets'
                    });

                    router.get('bucketsController').set('content', App.Bucket.find());
                },
                enter: function () {
                    $('ul.nav li').removeClass('active');
                    $('#nav-buckets').addClass('active');
                    console.log('navigating to buckets');
                }
            })
        })
    });

    App.initialize();
    window.App = App;
});

