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