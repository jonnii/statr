﻿
App.ApplicationController = Em.Controller.extend();
App.ApplicationView = Em.View.extend({
    templateName: 'application'
});

App.NavigationController = Em.Controller.extend();
App.NavigationView = Em.View.extend({
    templateName: 'navigation'
});

// <reference path="models/bucket.js"/>

// <reference path="views/footer.js"/>
// <reference path="views/home.js"/>

// <reference path="views/bucket.js"/>
// <reference path="controllers/bucket.js"/>
// <reference path="controllers/home.js"/>


App.ConfigurationController = Em.Controller.extend();
App.ConfigurationView = Em.View.extend({
    templateName: 'configuration'
});

App.BucketsController = Em.ArrayController.extend();
App.BucketsView = Em.View.extend({
    templateName: 'buckets'
});

App.Config = DS.Model.extend({
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

                router.get('configurationController').set('content', App.Config.find('current'));
            },
            enter: function () {
                $('ul.nav li').removeClass('active');
                $('#nav-configuration').addClass('active');
            }
        }),

        buckets: Em.Route.extend({
            route: '/buckets',
            showBucket: Em.Route.transitionTo('bucket'),
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
        }),

        bucket: Em.Route.extend({
            route: '/bucket/:bucket_id',
            connectOutlets: function (router, bucket) {
                router.get('applicationController').connectOutlet('bucket', bucket);
            }
        })
    })
});
