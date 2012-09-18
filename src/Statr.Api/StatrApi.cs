using System;
using System.Collections.Generic;
using SpeakEasy;
using Statr.Api.Models;

namespace Statr.Api
{
    public class StatrApi : IStatrApi
    {
        private readonly Lazy<IHttpClient> client;

        public StatrApi()
        {
            client = new Lazy<IHttpClient>(CreateClient);
        }

        public string RootUrl { get; set; }

        public IHttpClient Client
        {
            get { return client.Value; }
        }

        private IHttpClient CreateClient()
        {
            return HttpClient.Create(RootUrl);
        }

        public Config Config()
        {
            return Client.Get("config")
                .OnOk()
                .As<Config>();
        }

        public IEnumerable<Bucket> Buckets()
        {
            return Client.Get("buckets")
                .OnOk()
                .As<List<Bucket>>();
        }

        public IEnumerable<DataPoint> DataPoints(string bucket, string metricType)
        {
            return Client.Get("datapoints/:bucket/:metricType", new { bucket, metricType })
                .OnOk()
                .As<List<DataPoint>>();
        }
    }
}