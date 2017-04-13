using System.Collections.Generic; 

namespace Titan.ExpressionAnalyse
{
    public class SqlAnalyseResult
    {

        public SqlAnalyseResult()
        {
            SqlColumns = new Dictionary<IPropertyExpression, SqlColumn>();
            SortedOutputColumns = new List<string>();
            ForeignTables = new List<SqlTable>();
            ObjectFiller = new ObjectFiller();
        }

        /// <summary>
        /// 以属性的全路径作为键，例如"Person.City.CityName"
        /// </summary> 
        public Dictionary<IPropertyExpression, SqlColumn> SqlColumns { get; set; }

        /// <summary>
        /// 填充信息
        /// </summary>
        public ObjectFiller ObjectFiller { get; set; }
         
        /// <summary>
        /// 按outputColumnIndex排序的，因为在字段输出时需要
        /// 只会在拼接select部分的时候用到,
        /// 格式如：t1.Id f1
        /// </summary>
        public List<string> SortedOutputColumns { get; set; } 


        /// <summary>
        /// 主表的配型
        /// </summary>
        public object MasterEntityType { get; set; }

        /// <summary>
        /// 主表的别名，如"t1"
        /// </summary>
        public string MasterTableNameAlias { get; set; }

         
        public List<SqlTable> ForeignTables { get; set; }

          

        /// <summary>
        /// 是否有汇总函数
        /// </summary>
        public bool HasGroup { get; set; }
    }
}
