using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Configuration.Interfaces
{
    /// <summary>
    /// Configuratoin manager Interface
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets a Value casted to T Type for the given key or default if not exists
        /// </summary>
        /// <typeparam name="T">Type to cast value</typeparam>
        /// <param name="module">module to search key</param>
        /// <param name="key">key to search</param>
        /// <param name="defaultValue">default value to return if key does not exist</param>
        /// <returns></returns>
        T GetDefault<T>(string module, object key, T defaultValue);

        /// <summary>
        /// Gets a value casted to T Type for the given key
        /// </summary>
        /// <typeparam name="T">Type to cast value</typeparam>
        /// <param name="module">module to search key</param>
        /// <param name="key">key to search</param>
        /// <returns></returns>
        T GetKeyValue<T>(string module, object key);

        /// <summary>
        /// Verifies if a key exists in the current module
        /// </summary>
        /// <param name="module">module to search key</param>
        /// <param name="key">key to search</param>
        /// <returns></returns>
        bool ExistKey(string module, string key);
    }
}
