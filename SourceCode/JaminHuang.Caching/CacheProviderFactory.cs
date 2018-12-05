using JaminHuang.Caching.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching
{
    /// <summary>
    /// 缓存提供程序工厂类，提供一组获取、配置缓存提供程序的静态方法
    /// </summary>
    public static class CacheProviderFactory
    {
        /// <summary>
        /// The section.
        /// </summary>
        private static CacheSection section = (CacheSection)System.Configuration.ConfigurationManager.GetSection("JaminHuang.Caching");

        /// <summary>
        /// The provider.
        /// </summary>
        private static Lazy<ICacheProvider> provider = new Lazy<ICacheProvider>(() => GetProvider(section.Providers.DefaultProvider));

        /// <summary>
        /// The providers.
        /// </summary>
        private static Lazy<Dictionary<string, ICacheProvider>> providers = new Lazy<Dictionary<string, ICacheProvider>>(() => new Dictionary<string, ICacheProvider>());

        /// <summary>
        /// 获取默认缓存提供程序
        /// </summary>
        public static ICacheProvider DefaultProvider
        {
            get { return provider.Value; }
        }

        /// <summary>
        /// 获取指定名称的缓存提供程序
        /// </summary>
        /// <param name="name">提供程序名称</param>
        /// <returns></returns>
        public static ICacheProvider GetProvider(string name)
        {
            if (providers.Value.ContainsKey(name))
            {
                return providers.Value[name];
            }
            var configElement = section.Providers.GetByName(name);
            if (configElement != null)
            {
                Type type = Type.GetType(configElement.Provider);
                if (null != type)
                {
                    var p =
                        Activator.CreateInstance(type, configElement.ConfigData) as
                        ICacheProvider;
                    if (p == null)
                    {
                        throw new Exception(string.Format("无法创建名为{0}的提供程序", name));
                    }
                    providers.Value[name] = p;
                    return p;
                }
            }
            throw new Exception(string.Format("无法创建名为{0}的提供程序", name));
        }


    }
}
