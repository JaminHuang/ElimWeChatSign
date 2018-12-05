using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching.Redis
{
    /// <summary>
    /// redis缓存提供程序
    /// </summary>
    public class RedisCacheProvider : ICacheProvider
    {
        /// <summary>
        /// dbIndex
        /// </summary>
        private int dbIndex = 0;

        /// <summary>
        /// The redis.
        /// </summary>
        private ConnectionMultiplexer redis;

        /// <summary>
        /// 实例化提供程序
        /// </summary>
        /// <param name="config">连接字符串</param>
        /// <param name="db">索引</param>
        public RedisCacheProvider(string config, int db = 0)
        {
            this.dbIndex = RedisCacheConfig.DbIndex;
            this.redis = ConnectionMultiplexer.Connect(config);
        }

        /// <summary>
        /// 根据配置字典实例化提供程序
        /// </summary>
        /// <param name="configData">
        /// 根据key值为（db数据库序号）、（config连接字符串）的配置
        /// </param>
        public RedisCacheProvider(Dictionary<string, string> configData)
        {
            int.TryParse(configData["db"], out this.dbIndex);
            this.redis = ConnectionMultiplexer.Connect(configData["config"]);
        }

        /// <summary>
        /// 提供程序名称固定值RedisCacheProvider
        /// </summary>
        public string Name
        {
            get { return "RedisCacheProvider"; }
        }

        /// <summary>
        /// Gets the db.
        /// </summary>
        public IDatabase Db
        {
            get { return this.GetDb(); }
        }

        ///// <summary>
        ///// 获取缓存值
        ///// </summary>
        ///// <param name="key">缓存key</param>
        ///// <returns></returns>
        ////public object Get(string key)
        ////{
        ////    return this.Db.Get(key);
        ////}

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return this.Db.Get<T>(key);
        }

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值得类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireAt">过期时间</param>
        public void Set<T>(string key, T value, DateTime expireAt)
        {
            this.Db.Set(key, value, expireAt);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        public void Remove(string key)
        {
            this.Db.KeyDelete(key);
        }

        /////// <summary>
        /////// 异步获取缓存值
        /////// </summary>
        /////// <param name="key">缓存key</param>
        /////// <returns></returns>
        ////public async Task<object> GetAsync(string key)
        ////{
        ////    return await this.Db.GetAsync(key);
        ////}

        /// <summary>
        /// 异步获取缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await this.Db.GetAsync<T>(key);
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="T">设置值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireAt">过期时间</param>
        /// <returns></returns>
        public Task SetAsync<T>(string key, T value, DateTime expireAt)
        {
            return this.Db.SetAsync(key, value, expireAt);
        }

        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            await this.Db.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值得类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        public void Set<T>(string key, T value)
        {
            this.Db.Set(key, value);
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="T">设置值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public Task SetAsync<T>(string key, T value)
        {
            return this.Db.SetAsync(key, value);
        }

        /// <summary>
        /// 添加右侧队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush(string key, RedisValue value)
        {
            return this.Db.ListRightPush(key, value);
        }

        /// <summary>
        /// 添加左侧队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush(string key, RedisValue value)
        {
            return this.Db.ListLeftPush(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public void ListTrim(string key, long start, long stop)
        {
            this.Db.ListTrim(key, start, stop);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public long ListLength(string key)
        {
            return this.Db.ListLength(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public RedisValue ListGetByIndex(string key, long index)
        {
            return this.Db.ListGetByIndex(key, index);
        }

        /// <summary>
        /// 获取实例的DB
        /// </summary>
        /// <returns></returns>
        private IDatabase GetDb()
        {
            return this.redis.GetDatabase(this.dbIndex);
        }
    }
}
