 

namespace Titan.ExpressionAnalyse
{
    public class SqlColumn
    { 

        //主键
        public IPropertyExpression FullPropertyName { get; set; }

        ///// <summary>
        ///// 承载列的信息，必须有，比如针对SqlServer可能有IsNText属性
        ///// </summary>
        public IColumn Column { get; set; }
         


        /// <summary>
        /// 表的别名和列的标准名称，例如"t1.CityId","sum(t1.CityId)"
        /// </summary> 
        public string TableAliasAndColumn { get; set; }



    }
}
