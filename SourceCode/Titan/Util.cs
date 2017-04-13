using System;
using System.Collections.Generic;
using System.Reflection;

namespace Titan
{
    /// <summary>
    /// 封装一些常用函数
    /// </summary>
    public static class Util
    {
        public static DateTime DefaultDateTime = new DateTime();

        private static readonly Dictionary<Type, object> Caches = new Dictionary<Type, object>();
        private static readonly MethodInfo GetDefaultValueMethod = typeof(Util).GetMethod("D", BindingFlags.Static | BindingFlags.NonPublic);


        private static T D<T>()
        {
            return default(T);
        }


        public static object GetDefaultValue(Type type)
        {
            if (!Caches.ContainsKey(type))
            {
                lock (Caches)
                {
                    if (!Caches.ContainsKey(type))
                    {
                        MethodInfo getDefaultValueMethodGeneric = GetDefaultValueMethod.MakeGenericMethod(type);
                        object value = getDefaultValueMethodGeneric.Invoke(null, new object[] { });
                        Caches.Add(type, value);
                    }
                }
            }
            return Caches[type];
        }

        //public static Type GetDeclaredType<T>(T self)
        //{
        //    return typeof(T);
        //}

        ///// <summary>
        ///// 从某个集合中排除一些对象，不区分大小写
        ///// </summary>
        ///// <param name="original">原始对象集合</param>
        ///// <param name="exclusion">需要排除的对象</param>
        ///// <returns></returns>
        //public static IEnumerable<string> Exclude(string[] original, IEnumerable<string> exclusion)
        //{
        //    if (exclusion == null) return original;

        //    HashSet<string> exclusionHash = new HashSet<string>(exclusion,StringComparer.OrdinalIgnoreCase);//不区分大小写
        //    if (exclusionHash.Count == 0) return original;

        //    List<string> list = new List<string>(original.Length);
        //    foreach (string s in original)
        //    {
        //        if (!exclusionHash.Contains(s))
        //        {
        //            list.Add(s);
        //        }
        //    }
        //    return list;
        //}


        /// <summary>
        /// 检查pageSize和pageIndex 是否合法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        public static void CheckPageSizeAndPageIndex(int? pageSize, int? pageIndex)
        {
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                if (pageSize < 0) throw PageSizeArgumentOutOfRangeException();
                if (pageIndex < 1) throw PageIndexArgumentOutOfRangeException();
            }
        }
        private static ArgumentOutOfRangeException PageIndexArgumentOutOfRangeException()
        {
            return new ArgumentOutOfRangeException("pageIndex", "pageIndex从1开始，参数必须>=1");
        }
        private static ArgumentOutOfRangeException PageSizeArgumentOutOfRangeException()
        {
            return new ArgumentOutOfRangeException("pageSize", "pageSize必须>=0");
        }

        private static void CollectNames(List<string> list, object obj)
        {
            if (obj == null) return;

            if (obj is IEnumerable<object>)
            {
                foreach (object subobj in (obj as IEnumerable<object>))
                {
                    CollectNames(list, subobj);
                }
            } 
            else if (obj is IOutputExpression)
            {
                list.Add((obj as IOutputExpression).Property.PropertyName);
            }
            else if (obj is IPropertyExpression)
            {
                list.Add((obj as IPropertyExpression).PropertyName);
            }
            else
            {
                list.Add(obj.ToString());
            }
        }
        public static List<string> CollectNames(object names)
        {
            List<string> list = new List<string>();
            CollectNames(list, names);
            return list;
        }

        internal static bool HasCondition(ICondition condition)
        {
            if (condition == null) return false;
            if (condition is IConditionExpressionCollection)
            {
                return ((IConditionExpressionCollection)condition).Count > 0;
            }
            return true;
        }
    }
}
