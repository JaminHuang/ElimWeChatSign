using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching
{
    /// <summary>
    /// 缓存提供程序接口
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 提供程序名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取指定类型的缓存值
        /// </summary>
        /// <typeparam name="T">要获取的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns>返回类型为T的缓存值，如果值不存在则返回default(T)</returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireAt">过期时间</param>
        void Set<T>(string key, T value, DateTime expireAt);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        void Remove(string key);

        /// <summary>
        /// 异步获取指定类型的缓存值
        /// </summary>
        /// <typeparam name="T">要获取的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns>返回类型为T的缓存值，如果值不存在则返回default(T)</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 异步设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value);

        /// <summary>
        /// 异步设置缓存值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireAt">过期时间</param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, DateTime expireAt);

        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task RemoveAsync(string key);
    }
}
