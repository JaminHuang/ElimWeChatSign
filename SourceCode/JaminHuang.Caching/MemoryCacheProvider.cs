using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching
{
    /// <summary>
    /// 基于MemoryCache实现的缓存提供程序，本提供程序不提供异步操作
    /// </summary>
    public class MemoryCacheProvider : ICacheProvider
    {
        /// <summary>
        /// The Cache.
        /// </summary>
        public readonly MemoryCache Cache = new MemoryCache("_ObjectCacheProvider");

        /// <summary>
        /// 获取提供程序名称，固定值MemoryCacheProvider
        /// </summary>
        public string Name
        {
            get { return "MemoryCacheProvider"; }
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <returns>
        /// 返回缓存值，如果不存在则返回null
        /// </returns>
        public object Get(string key)
        {
            return this.Cache.Get(key);
        }

        /// <summary>
        /// 获取指定类型的缓存值
        /// </summary>
        /// <typeparam name="T">
        /// 要获取的类型
        /// </typeparam>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <returns>
        /// 返回类型为T的缓存值，如果值不存在则返回default(T)
        /// </returns>
        public T Get<T>(string key)
        {
            var data = this.Cache.Get(key);
            if (data == null)
            {
                return default(T);
            }

            return (T)data;
        }

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">
        /// 缓存值的类型
        /// </typeparam>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <param name="value">
        /// 缓存值
        /// </param>
        /// <param name="expireAt">
        /// 过期时间
        /// </param>
        public void Set<T>(string key, T value, DateTime expireAt)
        {
            this.Cache.Set(key, value, new DateTimeOffset(expireAt));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">
        /// 缓存key
        /// </param>
        public void Remove(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// 异步获取缓存值
        /// </summary>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <returns>
        /// 返回缓存值，如果不存在则返回null
        /// </returns>
        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 异步获取指定类型的缓存值
        /// </summary>
        /// <typeparam name="T">
        /// 要获取的类型
        /// </typeparam>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <returns>
        /// 返回类型为T的缓存值，如果值不存在则返回default(T)
        /// </returns>
        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 异步设置缓存值
        /// </summary>
        /// <typeparam name="T">
        /// 缓存值的类型
        /// </typeparam>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <param name="value">
        /// 缓存值
        /// </param>
        /// <param name="expireAt">
        /// 过期时间
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task SetAsync<T>(string key, T value, DateTime expireAt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key">
        /// 缓存key
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        public void Set<T>(string key, T value)
        {
            this.Cache.Set(key, value, new DateTimeOffset(DateTime.Now.AddYears(1)));
        }

        /// <summary>
        /// The set async.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Exception
        /// </exception>
        public Task SetAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
