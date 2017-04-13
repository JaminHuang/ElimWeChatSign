using System;

namespace Titan
{
    public interface IPropertyExpression:IComparable
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string PropertyName { get; set; }



        /// <summary>
        /// 分组函数
        /// </summary>
        GroupFunction GroupFunction { get; set; }
    }
}
