using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Interfaces
{
    public interface IConfigurationParametersProvider
    {
        ConfigurationMode ConfigurationMode { get; }
        IPHostEntry HostName { get; }
        string ApplicationName { get; }
        string UserName { get; }
        string Password {get;}
        bool UseCache { get; }
    }
}
