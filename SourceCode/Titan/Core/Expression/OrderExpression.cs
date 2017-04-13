

using System.Runtime.Serialization;
namespace Titan
{
    /// <summary>
    /// 
    /// </summary> 
    [DataContract]
    public class OrderExpression : IOrderExpression
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderExpression()
        {
            OrderType = OrderType.Asc;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="orderType">排序类型</param>
        public OrderExpression(string propertyName, OrderType orderType)
        {
            Property = new PropertyExpression(propertyName);
            OrderType = orderType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="groupFunction"></param>
        /// <param name="orderType">排序类型</param>
        public OrderExpression(string propertyName,GroupFunction groupFunction, OrderType orderType)
        {
            Property = new PropertyExpression(propertyName, groupFunction);
            OrderType = orderType;
        }

        #endregion

        /// <summary>
        /// 输出的属性的名称
        /// </summary> 
        [DataMember]
        public PropertyExpression Property { get; set; }



        /// <summary>
        /// 排序类型，升序或者降序
        /// </summary> 
        [DataMember]
        public OrderType OrderType { get; set; }

         


        public override string ToString()
        {
            return Property + " " + OrderType.ToString();
        }

        IPropertyExpression IOrderExpression.Property
        {
            get
            {
                return Property;
            }
            set
            {
                Property=(PropertyExpression)value;
            }
        }
    }




}
