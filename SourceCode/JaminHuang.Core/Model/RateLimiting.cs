using System;
using System.Collections.Generic;
using JaminHuang.Util;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Core.Model
{
    /// <summary>
    /// 访问限制验证
    /// </summary>
    public class RateLimiting
    {
        public static bool Sms(string ip)
        {
            if (ip.Trim() == "") return true;
            string key = "Limiting.Sms:" + ip;
            return Limit(key, 3600, 50, 3600);
        }

        /// <summary>
        /// 短信验证限制
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="second">间隔</param>
        /// <param name="count">次数</param>
        /// <param name="banned">禁止数</param>
        /// <returns></returns>
        private static bool Limit(string key, int second, int count, int banned = 0)
        {
            try
            {
                if (Caching.CacheClient.ListLength<List<string>>(key) < count)
                {
                    Caching.CacheClient.ListLeftPush(key, DateTime.UtcNow.ToTimestamp().ToString(), DateTime.MaxValue);
                    return true;
                }
                else
                {
                    long time = long.Parse(Caching.CacheClient.ListGetByIndex<string>(key, -1));
                    if (DateTime.UtcNow.ToTimestamp() - time < second)
                    {
                        Caching.CacheClient.ListTrim<string>(key, 0, count - 2);
                        if (banned > 0)
                            Caching.CacheClient.ListRightPush(key, (DateTime.UtcNow.ToTimestamp() + banned).ToString(), DateTime.MaxValue);
                        return false;
                    }
                    else
                    {
                        Caching.CacheClient.ListLeftPush(key, DateTime.UtcNow.ToTimestamp().ToString(), DateTime.MaxValue);
                        Caching.CacheClient.ListTrim<string>(key, 0, count - 1);
                        return true;
                    }
                }
            }
            catch { return true; }
        }
    }
}
