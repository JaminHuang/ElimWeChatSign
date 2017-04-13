 

namespace Titan
{
    /// <summary>
    /// 告诉系统生成insert,update 语句时“value”中如何指定
    /// </summary>
    public enum ValueBehavior
    {
        /// <summary>
        /// 使用对象属性中的值
        /// </summary>
        UsePropertyValue = 1,
        /// <summary>
        /// 使用固定值表达式
        /// </summary>
        UseValueExpression = 2,
    }
}
