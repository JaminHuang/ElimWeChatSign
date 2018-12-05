using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaminHuang.Caching
{
    /// <summary>
    /// 缓存客户端
    /// </summary>
    public static class CacheClient
    {

        /// <summary>
        /// 获取缓存提供程序的名称
        /// </summary>
        public static string ProviderName
        {
            get { return Provider.Name; }
        }

        /// <summary>
        /// 获取提供程序
        /// </summary>
        private static ICacheProvider Provider
        {
            get { return CacheProviderFactory.DefaultProvider; }
        }

        /// <summary>
        /// 获取特定类型缓存值，如果为空则返回default(T)
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return CacheProviderFactory.DefaultProvider.Get<T>(key);
        }

        /// <summary>
        /// 异步获取特定类型的缓存值，如果为空则返回default(T)
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public static Task<T> GetAsync<T>(string key)
        {
            return CacheProviderFactory.DefaultProvider.GetAsync<T>(key);
        }

        /// <summary>
        /// 获取特定类型的缓存值，如果为空则设置新值并返回
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="fun">当缓存值为空时，用来生成新值得方法</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static T Get<T>(string key, Func<T> fun, DateTime expire)
        {
            var value = Provider.Get<T>(key);
            if (value == null)
            {
                value = fun();
                Provider.SetAsync(key, value, expire);
            }
            return (T)value;
        }

        public static T Get<T>(string key, Func<object, T> fun, object funParm, DateTime expire)
        {
            var v = Provider.Get<T>(key);
            if (v == null)
            {
                v = fun(funParm);
                Provider.SetAsync<T>(key, v, expire);
            }
            return (T)v;
        }

        /// <summary>
        /// 获取特定类型的缓存值，如果为空则设置新值并返回
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="fun">当缓存值为空时，用来生成新值的方法</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static T Get<T>(string key, Func<T> fun, TimeSpan expire)
        {
            return Get<T>(key, fun, DateTime.Now.Add(expire));
        }

        /// <summary>
        /// 异步获取特定类型的缓存值，如果为空则设置新值并返回
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="fun">当缓存值为空时，用来生成新值的方法</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string key, Func<T> fun, DateTime expire)
        {
            var v = await Provider.GetAsync<T>(key);
            if (v == null)
            {
                v = fun();
                await Provider.SetAsync<T>(key, v, expire);
            }
            return (T)v;
        }

        /// <summary>
        /// 异步获取特定类型的缓存值，如果为空则设置新值并返回
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="fun">当缓存值为空时，用来生成新值的方法</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string key, Func<T> fun, TimeSpan expire)
        {
            return await GetAsync(key, fun, DateTime.Now.Add(expire));
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
        public static void Set<T>(string key, T value)
        {
            CacheProviderFactory.DefaultProvider.Set(key, value);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        public static void Set<T>(string key, T value, DateTime expire)
        {
            CacheProviderFactory.DefaultProvider.Set(key, value, expire);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        public static void Set<T>(string key, T value, TimeSpan expire)
        {
            CacheProviderFactory.DefaultProvider.Set(key, value, DateTime.Now.Add(expire));
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static Task SetAsync<T>(string key, T value, DateTime expire)
        {
            return CacheProviderFactory.DefaultProvider.SetAsync(key, value, expire);
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public static Task SetAsync<T>(string key, T value, TimeSpan expire)
        {
            return CacheProviderFactory.DefaultProvider.SetAsync(key, value, DateTime.Now.Add(expire));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        public static void Remove(string key)
        {
            CacheProviderFactory.DefaultProvider.Remove(key);
        }

        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public static Task RemoveAsync(string key)
        {
            return CacheProviderFactory.DefaultProvider.RemoveAsync(key);
        }

        #region 扩展方法

        /// <summary>
        /// List左侧添加元素
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        public static void ListLeftPush<T>(string key, T value, DateTime expire)
        {
            var list = Get<List<T>>(key) ?? new List<T>();
            list.Insert(0, value);

            Set(key, list);
        }

        /// <summary>
        /// List右侧添加元素
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        public static void ListRightPush<T>(string key, T value, DateTime expire)
        {
            var list = Get<List<T>>(key) ?? new List<T>();
            list.Add(value);
            //重新设置
            Set(key, list);
        }

        /// <summary>
        /// List删除指定元素
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expire">过期时间</param>
        public static void ListRemoveItem<T>(string key, T value, DateTime expire)
        {
            var list = Get<List<T>>(key) ?? new List<T>();
            list.Remove(value);
            //重新设置
            Set(key, list);
        }

        /// <summary>
        /// 获取指定元素
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="index">-1 为最后一个</param>
        public static T ListGetByIndex<T>(string key, int index)
        {
            var list = Get<List<T>>(key);

            return index == -1 ? list[list.Count - 1] : list[index];
        }

        /// <summary>
        /// 获取指定key对象列表的总数
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="index">-1 为最后一个</param>
        public static int ListLength<T>(string key)
        {
            var list = Get<List<T>>(key);
            if (list == null)
            {
                return 0;
            }

            return list.Count;
        }

        /// <summary>
        /// 设置指定范围元素
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="index">-1 为最后一个</param>
        public static void ListTrim<T>(string key, int start, int end)
        {
            var list = Get<List<T>>(key);

            if (list != null)
            {
                var new_list = list.Skip(start).Take(end - start + 1);

                Set(key, new_list);
            }
        }

        #endregion
    }
}
