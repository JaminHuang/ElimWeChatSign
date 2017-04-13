using System.Collections.Generic; 

namespace Titan.ExpressionAnalyse
{

    /// <summary>
    /// 多表关联时描述数据库库相关表的信息
    /// </summary>
    public class SqlTable
    {

        /// <summary>
        /// 描述表之间关系字段
        /// </summary>
        public List<SqlRelationColumn> RelationColumns { get; set; }
	
	
	    /// <summary>
	    /// 由于表名称可能是动态外部传递进来，因此需要存储实体对象的类型
	    /// </summary>
        public object ForeignEntityType { get; set; }



        /// <summary>
        /// 表的别名，例如在生成select t1.* from table1 t1 left outer join table2 t2 on ....
        /// 本属性的值就是"t1"
        /// </summary>
        public string TableNameAlias { get; set; }

         

        /// <summary>
        /// 参考TableNameAlias属性，本属性的值就是外键表的别名"t2"
        /// </summary>
        public string ForeignTableNameAlias { get; set; }
    }
}
