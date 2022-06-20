using Newtonsoft.Json;
using ProvNet.Core.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Implementations
{
    public class JsonConfigurationClient : IConfigurationClient
    {
        private readonly IDictionary<string, IDictionary<string, string>> _configContent;

        public JsonConfigurationClient(string jsonConfigurationSourceFile)
        {
            CheckFile(jsonConfigurationSourceFile);
            _configContent = GetFileDictionaryRepresentation(jsonConfigurationSourceFile);
        }

        internal bool ExistKey(string module, string key)
        {
            return _configContent.ContainsKey(module) && _configContent[module].ContainsKey(key);
        }

        public void Dispose()
        {
            return;
        }

        public IDictionary<string, string> GetAllKeys(string module)
        {
            return _configContent[module];
        }

        public string GetValue(string module, string key)
        {
            return _configContent[module][key];
        }

        public void NotifyUpdate(string module, Action<KeyValuePair<string, string>> keyValueTuple)
        {
            throw new NotImplementedException();
        }

        private void CheckFile(string configurationClientJson)
        {
            if (!File.Exists(configurationClientJson))
            {
                throw new FileNotFoundException(configurationClientJson);
            }
        }

        private IDictionary<string, IDictionary<string, string>> GetFileDictionaryRepresentation(string fileName)
        {
            CheckFile(fileName);
            var fileContent = File.ReadAllText(fileName, Encoding.Default);

            var configDeserialize = JsonConvert.DeserializeObject<IDictionary<string, IDictionary<string, string>>>(fileContent);

            var node = (from module in configDeserialize.Keys
                        from key in configDeserialize[module].Keys
                        where configDeserialize[module][key] == "$MachineName$"
                        select new
                        {
                            module,
                            key
                        }).FirstOrDefault();

            if (node != null)
            {
                configDeserialize[node.module][node.key] = Environment.MachineName;
            }

            return ToCaseInsensitive(configDeserialize);
        }
        public IDictionary<string, IDictionary<string, string>> ToCaseInsensitive(IDictionary<string, IDictionary<string, string>> caseSensitiveDictionary)
        {
            var caseInsensitiveDictionary = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            caseSensitiveDictionary.Keys.ToList()
                .ForEach(k => caseInsensitiveDictionary[k] = ToCaseInsensitive(caseSensitiveDictionary[k]));

            return caseInsensitiveDictionary;
        }

        public Dictionary<string, string> ToCaseInsensitive(IDictionary<string, string> caseSensitiveDictionary)
        {
            var caseInsensitiveDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            caseSensitiveDictionary.Keys.ToList()
                .ForEach(k => caseInsensitiveDictionary[k] = caseSensitiveDictionary[k]);

            return caseInsensitiveDictionary;
        }
    }
}
