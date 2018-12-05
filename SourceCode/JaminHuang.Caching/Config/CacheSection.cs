using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Config
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class CacheSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the providers.
        /// </summary>
        [ConfigurationProperty("providers", IsRequired = true)]
        public CacheProviderCollection Providers
        {
            get
            {
                CacheProviderCollection providers = (CacheProviderCollection)base["providers"];
                return providers;
            }
        }
    }
}
