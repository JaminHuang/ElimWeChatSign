namespace Titan
{
    /// <summary>
    /// 描述一个输出字段
    /// </summary>
    public interface IOutputExpression
    {
        /// <summary>
        /// 属性表达式
        /// </summary>
        IPropertyExpression Property { get; set; }



        /// <summary>
        /// Sql语句中的别名
        /// </summary>
        string Alias { get; set; }
    }
}
