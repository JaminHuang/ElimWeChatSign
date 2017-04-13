using System;

namespace Titan
{

    /// <summary>
    /// 描述属性（数据库字段）的规则，存储所有数据库都有的属性，例如全文索引属性就不会在IColumn里出现，因为不是所有数据库都支持
    /// </summary>
    public interface IColumn
    {

        #region 不可覆盖属性 

        /// <summary>
        /// 是否主键，只允许在ColumnBase中赋值 
        /// </summary>
        bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否返回从数据库生成的值，一般应用于自动增长的列，如oracle中的序列，
        /// 只允许在ColumnBase中赋值，以保护不被其它Attribute覆盖，
        /// </summary>
        bool ReturnAfterInsert { get; set; }



        Type PropertyType { get; set; }



        /// <summary>
        /// 因为使用时要忽略大小写，必须存储一个大小写敏感的属性名称
        /// </summary>
        string PropertyName { get; set; }


        #endregion


        #region 可覆盖属性


        /// <summary>
        /// 对应的数据库字段名，例如“[CustomerName]”
        /// </summary>
        string ColumnName { get; set; }


        /// <summary>
        /// 别名，仅用于在存储过程返回DataReader时的字段名称 
        /// 与ColumnName属性的区别：ColumnName属性总大多用于生成sql语句,而Alias则只用于存储过程返回DataReader时的字段名称 
        /// </summary>
        string OutputAlias { get; set; } 



        /// <summary>
        /// 全文索引
        /// Oracle如："contains","catsearch","function({column},{parameter},1)>0"
        /// SqlServer如："contains"
        /// </summary>
        string FullTextSearch { get; set; }



        /// <summary>
        /// 生成insert语句时使用的规则
        /// </summary>
        ColumnSqlBehavior InsertBehavior { get; set; }  


        /// <summary>
        /// 数据库字段长度
        /// </summary>
        int Size { get; set; } 

        #endregion




        ///// <summary>
        ///// 属性代理，有时Column标注在Property上，有时可能标注在Field上，通过这个代理对象可以方便获取或者设置对象属性
        ///// </summary>
        //PropertyAdapter PropertyAdapter { get; set; }



        
    }
 
}
