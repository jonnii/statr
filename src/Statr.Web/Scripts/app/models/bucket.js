App.Bucket = DS.Model.extend({
    primaryKey: 'Name',
    name: DS.attr('string', { key: 'Name' }),
    metricType: DS.attr('string', { key: 'MetricType' })
});