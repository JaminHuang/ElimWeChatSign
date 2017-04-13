
namespace Titan
{
    /// <summary>
    /// 描述排序规则
    /// </summary>
    public interface IOrderExpression
    {

        /// <summary>
        /// 需要排序的属性名称
        /// </summary>
        IPropertyExpression Property { get; set; }



        /// <summary>
        /// 升序还是降序
        /// </summary>
        OrderType OrderType { get; set; } 
    }
}