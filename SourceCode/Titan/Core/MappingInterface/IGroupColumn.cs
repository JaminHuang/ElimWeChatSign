using System;

namespace Titan
{

    /// <summary>
    /// 描述属性（数据库字段）的规则，存储所有数据库都有的属性，例如全文索引属性就不会在IColumn里出现，因为不是所有数据库都支持
    /// </summary>
    public interface IGroupColumn
    {
         

        /// <summary>
        /// 
        /// </summary>
        GroupFunction GroupFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string OriginalPropertyName { get; set; }



        string PropertyName { get; set; }

        Type PropertyType { get; set; }

        ///// <summary>
        ///// 属性代理，有时Column标注在Property上，有时可能标注在Field上，通过这个代理对象可以方便获取或者设置对象属性
        ///// </summary>
        //PropertyAdapter PropertyAdapter { get; set; }



        
    }
 
}
