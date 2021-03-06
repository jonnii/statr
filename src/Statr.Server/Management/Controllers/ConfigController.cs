﻿using System.Web.Http;
using Castle.Core.Logging;
using Statr.Server.Configuration;

namespace Statr.Server.Management.Controllers
{
    public class ConfigController : ApiController
    {
        private readonly IConfigRepository configRepository;

        public ConfigController(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public Config Get()
        {
            Logger.Info("Getting configuration");

            return configRepository.GetConfiguration();
        }
    }
}
