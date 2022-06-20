using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Interfaces
{
    public interface IConfigurationClient
    {
        IDictionary<string, string> GetAllKeys(string module);
        string GetValue(string module, string key);
        void NotifyUpdate(string module, Action<KeyValuePair<string, string>> keyValueTuple);
    }
}
