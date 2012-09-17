using System;
using System.Collections.Generic;
using SpeakEasy;
using Statr.Configuration;

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

        public Config GetConfig()
        {
            return Client.Get("config").OnOk().As<Config>();
        }

        //public IEnumerable<Bucket> GetBuckets()
        //{
        //    return Client.Get("buckets").OnOk().As<List<Bucket>>();
        //}
    }
}