App.BucketView = Em.View.extend({
    graph: null,
    templateName: 'bucket',
    didInsertElement: function () {
        console.log("setting up graph");

        this.graph = new Rickshaw.Graph({
            element: document.querySelector("#data-points-graph"),
            height: 400,
            renderer: 'area',
            series: new Rickshaw.Series.FixedDuration([{ name: 'one', color: 'lightblue' }], undefined, {
                timeInterval: 250,
                maxDataPoints: 400,
                timeBase: new Date().getTime() / 1000
            })
        });

        var xAxis = new Rickshaw.Graph.Axis.Time({
            graph: this.graph
        });

        xAxis.render();

        var yAxis = new Rickshaw.Graph.Axis.Y({
            graph: this.graph
        });

        yAxis.render();

        this.graph.render();
    },
    initialDataLoaded: function () {
        var graph = this.get('graph');

        if (graph == null) {
            return;
        }

        var dataPoints = this.get('controller')
            .get('dataPoints');

        var values = $.map(dataPoints, function (d) {
            return 0.0 + d.Value;
        });

        console.log("adding new values: " + values.length);

        $.each(values, function (i, v) {
            var data = { one: v };
            graph.series.addData(data);
        });

        graph.render();

    }.observes('controller.dataPoints.@each'),

    pushPoint: function () {
        var lastMetric = this.get('controller').get('lastMetric');
        var graph = this.get('graph');
        if (graph == null) {
            return;
        }
        var data = { one: lastMetric };
        graph.series.addData(data);
        graph.render();
    }.observes('controller.lastMetric')
});