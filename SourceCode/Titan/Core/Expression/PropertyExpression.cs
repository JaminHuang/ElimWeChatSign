using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    public class PropertyExpression : IPropertyExpression
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PropertyExpression()
        {
            GroupFunction = GroupFunction.None;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param> 
        public PropertyExpression(string propertyName)
        {
            PropertyName = propertyName;
            GroupFunction = GroupFunction.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="groupFunction"></param> 
        public PropertyExpression(string propertyName, GroupFunction groupFunction)
        {
            PropertyName = propertyName;
            GroupFunction = groupFunction;
        }

        #endregion


        private int hash;
        private void setHash()
        {
            hash = propertyName == null ? groupFunction.GetHashCode() : propertyName.ToLower().GetHashCode() ^ groupFunction.GetHashCode();
        }
        private string propertyName;
        private GroupFunction groupFunction=GroupFunction.None;
        /// <summary>
        /// 输出的属性的名称
        /// </summary> 
        [DataMember]
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
                setHash();
            }
        }



        /// <summary>
        /// 排序类型，升序或者降序
        /// </summary> 
        [DataMember]
        public GroupFunction GroupFunction
        {
            get
            {
                return groupFunction;
            }
            set
            {
                groupFunction = value;
                setHash();
            }
        }

        #region 快速条件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TCustom(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.Custom, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TEqual(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.Equal, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TGreaterThanOrEqual(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.GreaterThanOrEqual, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TGreaterThan(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.GreaterThan, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TIn(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.In, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TLessThanOrEqual(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.LessThanOrEqual, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TLessThan(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.LessThan, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.Like, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TNotEqual(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotEqual, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TNotIn(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotIn, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TNotLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotLike, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TLeftLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.LeftLike, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TLeftNotLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotLeftLike, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TRightLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.RightLike, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TRightNotLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotRightLike, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TFullTextLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.FullTextLike, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionExpression TFullTextNotLike(object value)
        {
            return new ConditionExpression(PropertyName, GroupFunction, ConditionOperator.NotFullTextLike, value);
        }


        public static PropertyExpression Count { get { return new PropertyExpression("***", GroupFunction.Count); } }
        public PropertyExpression Sum { get { return new PropertyExpression(PropertyName, GroupFunction.Sum); } }
        public PropertyExpression Avg { get { return new PropertyExpression(PropertyName, GroupFunction.Avg); } }
        public PropertyExpression Max { get { return new PropertyExpression(PropertyName, GroupFunction.Max); } }
        public PropertyExpression Min { get { return new PropertyExpression(PropertyName, GroupFunction.Min); } }

        public OrderExpression Asc { get { return new OrderExpression(PropertyName, GroupFunction, OrderType.Asc); } }
        public OrderExpression Desc { get { return new OrderExpression(PropertyName, GroupFunction, OrderType.Desc); } }
        #endregion

        #region 运算符重载
        public static ConditionExpression operator ==(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.Equal, value);
        }
        public static ConditionExpression operator !=(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.NotEqual, value);
        }
        public static ConditionExpression operator >(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.GreaterThan, value);
        }
        public static ConditionExpression operator >=(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.GreaterThanOrEqual, value);
        }
        public static ConditionExpression operator <(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.LessThan, value);
        }
        public static ConditionExpression operator <=(PropertyExpression left, object value)
        {
            return new ConditionExpression(left.PropertyName, left.GroupFunction, ConditionOperator.LessThanOrEqual, value);
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return hash;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return base.Equals(obj);
            }
            else
            {
                return GetHashCode().Equals(obj.GetHashCode());
            }
        }

        public override string ToString()
        {
            return GroupFunction == GroupFunction.None ? PropertyName : (GroupFunction.ToString() + "(" + PropertyName + ")");
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            IPropertyExpression p = (IPropertyExpression)obj;
            return GetHashCode().CompareTo(obj.GetHashCode());
        }
    }
}
