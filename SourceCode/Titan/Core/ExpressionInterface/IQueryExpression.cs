namespace Titan
{
    /// <summary>
    /// 查询表达式，以对象化的方式描述一个查询行为，内部根据这个类生成相应的sql语句
    /// </summary>
    public interface IQueryExpression
    {
        /// <summary>
        /// 暂不支持EntityType类型
        /// </summary>
        object EntityType { get; set; } 


        /// <summary>
        /// 是否去重复
        /// </summary>
        bool IsDistinct { get; set; }


        /// <summary>
        /// 相当于sql语句中的select部分 
        /// </summary>
        IOutputExpressionCollection Selects { get; set; }


        /// <summary>
        /// 相当于sql语句中的where部分 
        /// </summary>
        IConditionExpressionCollection Wheres { get; set; }


        /// <summary>
        /// 相当于sql语句中的order by部分 
        /// </summary>
        IOrderExpressionCollection OrderBys { get; set; }


        /// <summary>
        /// 相当于sql语句中的group by部分 
        /// </summary>
        IGroupExpressionCollection GroupBys { get; set; }


        /// <summary>
        /// 相当于sql语句中的having部分 
        /// </summary>
        IConditionExpressionCollection Havings { get; set; }


        /// <summary>
        /// 每页多少记录，PageSize和PageIndex只要有一个为null，则返回所有记录
        /// </summary> 
        int? PageSize { get; set; }


        /// <summary>
        /// 第几页开始，从1开始计数，PageSize和PageIndex只要有一个为null，则返回所有记录
        /// </summary> 
        int? PageIndex { get; set; }


        /// <summary>
        /// 是否计算符合条件的所有记录数，如果这个属性为true会生成类似"select count(...) from ... where ..."的语句
        /// </summary> 
        bool ReturnMatchedCount { get; set; }

 

    }
}
