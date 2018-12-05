using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Config
{
    /// <summary>
    ///     The Cache provider collection.
    /// </summary>
    public class CacheProviderCollection : ConfigurationElementCollection
    {

        /// <summary>
        ///     Gets the default provider.
        /// </summary>
        [ConfigurationProperty("defaultProvider", IsRequired = true)]
        public string DefaultProvider
        {
            get
            {
                var defaultProvider = (string)this["defaultProvider"];
                if (string.IsNullOrEmpty(defaultProvider))
                {
                    defaultProvider = "MemoryCacheProvider";
                }

                return defaultProvider;
            }
        }

        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="CacheProviderElement"/>.
        /// </returns>
        public CacheProviderElement GetByName(string name)
        {
            return (CacheProviderElement)this.BaseGet(name);
        }

        /// <summary>
        ///     The create new element.
        /// </summary>
        /// <returns>
        ///     The <see cref="ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheProviderElement();
        }

        /// <summary>
        /// The get element key.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheProviderElement)element).Name;
        }
    }
}
