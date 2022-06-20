using ProvNet.Core.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Implementations
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfigurationClient _configClient;

        public ConfigurationManager(IConfigurationClient configClient)
        {
            _configClient = configClient;
        }

        public bool ExistKey(string module, string key)
        {
            try
            {
                if (_configClient is JsonConfigurationClient)
                {
                    return (_configClient as JsonConfigurationClient).ExistKey(module, key);
                }
                return !string.IsNullOrEmpty(_configClient.GetValue(module, key));
            }
            catch
            {
                return false;
            }
        }

        public T GetKeyValue<T>(string module, object key)
        {
            var keystring = key.ToString();
            try
            {
                return ChangeType<T>(_configClient.GetValue(module, keystring));
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Key ({0}) not found in module ({1})", keystring, module));
            }
        }

        public T GetDefault<T>(string module, object key, T defaultValue)
        {
            var keystring = key.ToString();

            if (!ExistKey(module, key.ToString()))
            {
                return defaultValue;
            }

            return ChangeType<T>(_configClient.GetValue(module, keystring));
        }

        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }
    }
}
