using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Config
{
    public class CacheProviderElement : ConfigurationElement
    {
        /// <summary>
        /// The data.
        /// </summary>
        private Dictionary<string, string> data = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        [ConfigurationProperty("provider", IsRequired = false, DefaultValue = "JaminHuang.Caching.RedisCacheProvider,JaminHuang.Caching")]
        public string Provider
        {
            get { return (string)this["provider"]; }
            set { this["provider"] = value; }
        }

        /// <summary>
        /// The on deserialize unrecognized attribute.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            data[name] = value;
            return true;
        }

        /// <summary>
        /// Gets the config data.
        /// </summary>
        public Dictionary<string, string> ConfigData
        {
            get { return data; }
        }

    }
}
