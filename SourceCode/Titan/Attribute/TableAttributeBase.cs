using System;

namespace Titan
{
    /// <summary>
    /// 描述实体类的数据库表信息。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class TableAttributeBase : Attribute
    {

        /// <summary>
        /// 实体类说对应的数据库表的名称，如果为null或空则默认为实体类的名称。
        /// 建议将这个属性设置为空。
        /// </summary>
        public string TableName { get; set; }



    }
}
