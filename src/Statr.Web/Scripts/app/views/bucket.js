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