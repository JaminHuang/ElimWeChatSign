using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Redis
{
    /// <summary>
    /// The redis cache config.
    /// </summary>
    public static class RedisCacheConfig
    {
        /// <summary>
        /// The config.
        /// </summary>
        public static readonly string Config = ConfigurationManager.AppSettings["RedisCacheConfig"];

        /// <summary>
        /// The db index.
        /// </summary>
        public static readonly int DbIndex = Convert.ToInt32(ConfigurationManager.AppSettings["RedisCacheDb"] ?? "0");
    }
}
