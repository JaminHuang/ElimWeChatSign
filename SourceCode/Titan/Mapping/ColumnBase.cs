using System;

namespace Titan
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class ColumnBase : IColumn
    {
        #region 不可覆盖属性 
        public bool IsPrimaryKey { get; set; }
        public bool ReturnAfterInsert { get; set; }
        public Type PropertyType { get; set; }
        public string PropertyName { get; set; }
        #endregion


        #region 可覆盖属性
        public string ColumnName { get; set; }
        public string OutputAlias { get; set; }
        public string FullTextSearch { get; set; }
        public ColumnSqlBehavior InsertBehavior { get; set; }
        public int Size { get; set; }
        #endregion
         
         

    }

}
