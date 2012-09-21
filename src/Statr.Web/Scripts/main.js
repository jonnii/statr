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

    App.BucketController = Em.Controller.extend({
        dataPoints: Em.A([]),

        loadDataPoints: function () {
            var id = this.content.get('id');
            var metricType = this.content.get('metricType');

            if (id === null || metricType === null) {
                return;
            }

            console.log("getting initial datapoints");

            var that = this;
            Ember.$.get('/api/datapoints/' + metricType + '/' + id, function (e) {
                that.set('dataPoints', Em.A(e));
            }).then(function () {
                console.log("setting up data point data subscription");

                var subscriber = $.connection.dataPoints;
                subscriber.ack = function (message) {
                    console.log("confirming subscription to " + message);
                };

                subscriber.receive = function (message) {
                    that.get('dataPoints').pushObject(message);
                };

                console.log("registering for data points");
                $.connection.hub.start().done(function () {
                    var dataPoints = metricType + '/' + id;
                    console.log("subscribing to data points " + dataPoints);
                    subscriber.connect(dataPoints);
                });
            });

        }.observes('content.isLoaded')
    });

    App.BucketView = Em.View.extend({
        chart: null,
        templateName: 'bucket',
        didInsertElement: function () {
            console.log("setting up graph");
            var that = this;
            nv.addGraph(function () {
                var data = [{
                    values: [],
                    key: 'Things',
                    color: '#ff7f0e'
                }];

                that.chart = nv.models.lineChart();
                that.chart.xAxis
                    .axisLabel("Time")
                    .tickFormat(d3.format(',r'));

                that.chart.yAxis
                    .axisLabel("Value")
                    .tickFormat(d3.format('.02f'));

                d3.select("#data-points-graph")
                    .datum(data)
                    .transition().duration(500).call(that.chart);

                return that.chart;
            });
        },
        valueDidChange: function () {
            var chart = this.get('chart');

            if (chart == null) {
                return;
            }

            var dataPoints = this.get('controller')
                .get('dataPoints');

            var values = $.map(dataPoints, function (d) {
                return { x: d.TimeStamp, y: d.Value };
            });

            var data = [{
                values: values,
                key: 'Things',
                color: '#ff7f0e'
            }];

            d3.select('#data-points-graph')
                .datum(data)
                .transition().duration(500)
                .call(chart);
        }.observes('controller.dataPoints.@each')
    });

    App.Bucket = DS.Model.extend({
        primaryKey: 'Name',
        name: DS.attr('string', { key: 'Name' }),
        metricType: DS.attr('string', { key: 'MetricType' })
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

    App.initialize();
    window.App = App;
});

