using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JaminHuang.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class DataCacheAttribute : Attribute
    {
        public int CacheSeconds { get; set; }

        public DataCacheAttribute()
        {
            CacheSeconds = 60;
        }

        public DataCacheAttribute(int CacheSeconds)
        {
            this.CacheSeconds = CacheSeconds;
        }
    }

    public static class ActionInvoker
    {
        public static T Invoke<T>(object target, string actionName, Func<T> method, object @params = null, bool cancel = false)
        {
            if (cancel)
            {
                return method.Invoke();
            }

            var targetType = target.GetType();
            var targetInfo = targetType.GetMethod(actionName);

            if (targetInfo.IsDefined(typeof(DataCacheAttribute), true))
            {
                var attr = targetInfo
                    .GetCustomAttributes(typeof(DataCacheAttribute), true)
                    .OfType<DataCacheAttribute>()
                    .FirstOrDefault();

                StringBuilder sbKey = new StringBuilder();
                sbKey.Append(targetType.Name);
                sbKey.Append("-");
                sbKey.Append(actionName);

                if (@params != null)
                {
                    sbKey.Append("/");
                    var pros = @params.GetType().GetProperties();
                    foreach (var pro in pros.OrderBy(item => item.Name))
                    {
                        var value = pro.GetValue(@params);
                        if (value != null)
                        {
                            sbKey.Append(pro.Name);
                            sbKey.Append("-");
                            sbKey.Append(value.ToString());
                            sbKey.Append("-");
                        }
                    }
                }

                string key = sbKey.ToString();
                T data = CacheContainer.GetData<T>(key);
                if (data != null)
                {
                    return (T)data;
                }
                else
                {
                    var rsl = method.Invoke();

                    CacheContainer.SetData(key, rsl, attr.CacheSeconds);
                    return rsl;
                }
            }
            else
            {
                return method.Invoke();
            }

        }
    }

    public class DataCacheUnit
    {
        public object CacheData { get; set; }
        public DateTime LastCache { get; set; }
    }

    public static class CacheContainer
    {
        private static Hashtable Data = new Hashtable();
        private static Mutex DataLock = new Mutex();
        private const int MaxCacheCount = 3000;

        public static bool SetData(string key, object value, int seconds)
        {
            bool successLock = DataLock.WaitOne(30000);
            if (!successLock)
            {
                return false;
            }

            if (Data[key] != null)
            {
                if (value == null)
                {
                    Data.Remove(key);
                }
                else
                {
                    Data[key] = new DataCacheUnit
                    {
                        CacheData = value,
                        ExpTime = DateTime.Now.AddSeconds(seconds)
                    };
                }
            }
            else
            {
                if (Data.Count >= MaxCacheCount)
                {
                    DataLock.ReleaseMutex();
                    return false;
                }

                Data.Add(key, new DataCacheUnit
                {
                    CacheData = value,
                    ExpTime = DateTime.Now.AddSeconds(seconds)
                });
            }
            DataLock.ReleaseMutex();
            return true;
        }

        public static T GetData<T>(string key)
        {
            if (Data[key] == null)
            {
                return default(T);
            }

            if (DateTime.Now > ((DataCacheUnit)Data[key]).ExpTime)
            {
                return default(T);
            }
            else
            {
                return (T)((DataCacheUnit)Data[key]).CacheData;
            }
        }

        public static bool ClearUp(object target, string actionName)
        {
            bool successLock = DataLock.WaitOne(30000);
            if (!successLock)
            {
                return false;
            }

            var targetType = target.GetType();

            StringBuilder sbKey = new StringBuilder();
            sbKey.Append(targetType.Name);
            sbKey.Append("-");
            sbKey.Append(actionName);
            sbKey.Append("/");

            List<string> delKeyList = new List<string>();
            foreach (string key in Data.Keys)
            {
                if (key.Contains(sbKey.ToString()))
                {
                    delKeyList.Add(key);
                }
            }

            foreach (string key in delKeyList)
            {
                Data.Remove(key);
            }

            DataLock.ReleaseMutex();
            return true;
        }

        public static bool ClearUp()
        {
            bool successLock = DataLock.WaitOne(30000);
            if (!successLock)
            {
                return false;
            }

            DateTime now = DateTime.Now;
            List<string> delKeyList = new List<string>();
            foreach (string key in Data.Keys)
            {
                if (now > ((DataCacheUnit)Data[key]).ExpTime)
                {
                    delKeyList.Add(key);
                }
            }

            foreach (string key in delKeyList)
            {
                Data.Remove(key);
            }

            DataLock.ReleaseMutex();
            return true;
        }

        private class DataCacheUnit
        {
            public object CacheData { get; set; }
            public DateTime ExpTime { get; set; }
        }
    }
}
