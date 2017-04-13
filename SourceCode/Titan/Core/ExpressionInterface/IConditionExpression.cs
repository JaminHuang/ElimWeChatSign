

namespace Titan
{
    /// <summary>
    /// 描述一个条件项
    /// </summary>
    public interface IConditionExpression : ICondition
    {


        /// <summary>
        /// 属性名称
        /// </summary>
        IPropertyExpression Property { get; set; }



        /// <summary>
        /// 条件运算符
        /// </summary>
        ConditionOperator ConditionOperator { get; set; }


         


        /// <summary>
        /// 条件值
        /// </summary>
        object ConditionValue { get; set; }


    }
}
