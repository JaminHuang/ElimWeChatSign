using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Redis
{
    /// <summary>
    /// The redis extensions.
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDatabase cache, string key)
        {
            return Deserialize<T>(cache.StringGet(key));
        }

        /// <summary>
        /// 异步获取对象
        /// </summary>
        /// <typeparam name="T">Ojbect</typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDatabase cache, string key)
        {
            return Deserialize<T>(await cache.StringGetAsync(key));
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(this IDatabase cache, string key)
        {
            return Deserialize<object>(cache.StringGet(key));
        }

        /// <summary>
        /// 异步获取缓存值
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<object> GetAsync(this IDatabase cache, string key)
        {
            return Deserialize<object>(await cache.StringGetAsync(key));
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt"></param>
        /// <returns></returns>
        public static async Task SetAsync(this IDatabase cache, string key, object value, DateTime expireAt)
        {
            await cache.StringSetAsync(key, Serialize(value), expiry: expireAt - DateTime.Now);
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task SetAsync(this IDatabase cache, string key, object value)
        {
            await cache.StringSetAsync(key, Serialize(value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt"></param>
        public static void Set(this IDatabase cache, string key, object value, DateTime expireAt)
        {
            cache.StringSet(key, Serialize(value), expiry: expireAt - DateTime.Now);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(this IDatabase cache, string key, object value)
        {
            cache.StringSet(key, Serialize(value));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="o">Object</param>
        /// <returns></returns>
        private static string Serialize(object o)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };
            return o == null ? null : JsonConvert.SerializeObject(o, timeConverter);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        private static T Deserialize<T>(string json)
        {
            return string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
