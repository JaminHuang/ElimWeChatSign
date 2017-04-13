 

namespace Titan.ExpressionAnalyse
{
    public class SqlRelationColumn
    { 
        /// <summary>
        /// 主表的列标准名称，非别名
        /// </summary>
        public string ColumnName { get; set; }

         
        /// <summary>
        /// 外键表的列标准名称，非别名
        /// </summary>
        public string ForeignColumnName { get; set; }
         

         
        /// <summary>
        /// 是一个关系表达式
        /// {主表的别名}.{主表字段的标准名}={外键表的别名}.{外键字段的标准名}=如：t1.CityId=t2.CityId
        /// </summary>  
        public string Expression { get; set; }
    }
}
