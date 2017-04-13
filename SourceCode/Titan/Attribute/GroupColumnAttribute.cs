using System;

namespace Titan
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class GroupColumnAttribute:Attribute
    {
        /// <summary>
        /// 汇总函数的原始属性名称
        /// </summary>
        public string OriginalPropertyName { get; set; }

        /// <summary>
        /// 分组函数类型
        /// </summary>
        public GroupFunction GroupFunction { get; set; }
    }
}
