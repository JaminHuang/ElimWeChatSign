using System;


namespace Titan
{
    /// <summary>
    /// 标注实体类属性或字段，用来描述对应数据库列的规则。
    /// 不同数据库会有不同的特性，例如同一个属性在不同的数据库中对应的列名可能不相同。本类中的属性均可覆盖。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ColumnAttributeBase : Attribute
    {  

        #region 可覆盖成员



        /// <summary>
        /// 属性或字段所对应的数据库列名，如果为null或空则默认与属性名一致。
        /// 建议这个属性的值设置为null。
        /// 注意：针对不同数据库可能不同，应当在其它***ColumnAttribute中覆盖
        /// </summary>
        public string ColumnName { get; set; }




        /// <summary>
        /// 标注列的默认值，指定默认值的表达式名称，如“sys_date()”，生成insert的语句如：insert into table1 (f1,f2) values (:v1，sys_date())
        /// 注意：针对不同数据库可能不同，应当在其它***ColumnAttribute中覆盖
        /// </summary>
        public string DefaultValue { get; set; }



        private AttributeBoolean generateInsert = AttributeBoolean.Null;
        /// <summary>
        /// 标注是否要在生成Insert语句时包含这一列
        /// 注意：针对不同数据库可能不同，应当在其它***ColumnAttribute中覆盖
        /// </summary>
        public AttributeBoolean GenerateInsert { get { return generateInsert; } set { generateInsert = value; } }


        /// <summary>
        /// 标注在生成全文检索的sql语句时，使用什么命令，例如可以设置为"contains"，"catsearch"等
        /// 注意：针对不同数据库可能不同，应当在其它***ColumnAttribute中覆盖
        /// </summary>
        public string FullTextSearch { get; set; }




        private int size = -1;
        /// <summary>
        /// 数据库中列的长度
        /// </summary>
        public int Size { get { return size; } set { size = value; } }
        #endregion

         
    }
}
