using ProvNet.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Exceptions
{
    /// <summary>
    /// Exception generated by the application, This type of exception should have a special treatment in the consumer application.
    /// This type of exception support resource messages for different languages
    /// </summary>
    /// <typeparam name="T">Type of Resource Class</typeparam>
    public abstract class BusinessException<T> : Exception
    {
        protected string _resourceKey;
        protected object _values;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="resourceKey">Key in the Resource File for exception messages</param>
        /// <param name="values">parameters to replace in the resource value of the given key. 
        /// The resource value must be in the format "... {0} ... {1} ... {n} ..." depending on the quantity of values to replace</param>
        public BusinessException(string resourceKey, params string[] values)
        {
            _resourceKey = resourceKey;
            _values = values;
        }


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        /// <param name="resourceKey">Key in the Resource File for exception messages</param>
        /// <param name="values">parameters to replace in the resource value of the given key. 
        /// The resource value must be in the format "... {0} ... {1} ... {n} ..." depending on the quantity of values to replace</param>
        public BusinessException(Exception innerException, string resourceKey, params string[] values) : base(resourceKey, innerException)
        {
            _resourceKey = resourceKey;
            _values = values;
        }

        /// <summary>
        /// Ctro
        /// </summary>
        /// <param name="resourceKey">Key in the Resource File for exception messages</param>
        public BusinessException(string resourceKey)
        {
            _resourceKey = resourceKey;
            _values = null;
        }

        /// <summary>
        /// Return the Messages of The Exception in the different languages defined.
        /// </summary>
        public IList<Translation> Messages
        {
            get
            {
                var translations = new List<Translation>();

                var supportedCultures = new[]
                {
                    new CultureInfo("en-AR"),
                    new CultureInfo("es-US"),
                    new CultureInfo("pt-BR"),
                };

                var manager = new ResourceManager(typeof(T));

                foreach (var culture in supportedCultures)
                {
                    try
                    {
                        if (_values != null)
                        {
                            var message = string.Format(manager.GetString(_resourceKey, culture), (object[])_values);
                            translations.Add(new Translation(culture.Name, message));
                        }
                        else
                        {
                            var message = manager.GetString(_resourceKey, culture);
                            translations.Add(new Translation(culture.Name, message));
                        }
                    }
                    catch (Exception)
                    {
                        //TODO : Log Exception
                    }
                }
                return translations;
            }
        }
        public override string Message => Messages[0].Text;
    }
}
