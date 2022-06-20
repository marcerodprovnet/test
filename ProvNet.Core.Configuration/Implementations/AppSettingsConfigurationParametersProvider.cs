using Microsoft.Extensions.Configuration;
using ProvNet.Core.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Implementations
{
    /// <summary>
    /// Configuration Parameter Provisder For AppSettings.json
    /// </summary>
    public class AppSettingsConfigurationParametersProvider : IConfigurationParametersProvider
    {
        private readonly IConfiguration _configuration;

        public AppSettingsConfigurationParametersProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConfigurationMode ConfigurationMode => (ConfigurationMode)Enum.Parse(typeof(ConfigurationMode), _configuration.GetSection("ConfigurationClient:Mode").Value, true);

        public IPHostEntry HostName => new IPHostEntry() { HostName = _configuration.GetSection("ConfigurationClient:Hostname").Value };

        public string ApplicationName => _configuration.GetSection("ConfigurationClient:ApplicationName").Value;

        public string UserName => _configuration.GetSection("ConfigurationClient:Username").Value;

        public string Password => _configuration.GetSection("ConfigurationClient:Password").Value;

        public bool UseCache => Convert.ToBoolean(_configuration.GetSection("ConfigurationClient:UseCache").Value);
    }
}
