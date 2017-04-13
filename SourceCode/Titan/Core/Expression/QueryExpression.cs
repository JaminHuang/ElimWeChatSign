using System.Runtime.Serialization;

namespace Titan
{
    /// <summary>
    /// 对象输出规则
    /// </summary> 
    [DataContract]
    [KnownType(typeof(OutputExpressionCollection))]
    [KnownType(typeof(ConditionExpressionCollection))]
    [KnownType(typeof(OrderExpressionCollection))]
    public class QueryExpression : IQueryExpression
    {
        #region 属性
        /// <summary>
        /// 暂不支持EntityType类型
        /// </summary>
        [DataMember]
        public object EntityType { get; set; }


        /// <summary>
        /// 是否去重
        /// </summary>
        [DataMember]
        public bool IsDistinct { get; set; }

        /// <summary>
        /// 每页多少记录，PageSize和PageIndex只要有一个为null，则返回所有记录
        /// </summary> 
        [DataMember]
        public int? PageSize { get; set; }

        /// <summary>
        /// 第几页开始，从1开始计数，PageSize和PageIndex只要有一个为null，则返回所有记录
        /// </summary> 
        [DataMember]
        public int? PageIndex { get; set; }

        /// <summary>
        /// 是否计算符合条件的记录数
        /// </summary> 
        [DataMember]
        public bool ReturnMatchedCount { get; set; }



        private OutputExpressionCollection selects = new OutputExpressionCollection();
        /// <summary>
        /// 输出的属性集合
        /// </summary> 
        [DataMember]
        public OutputExpressionCollection Selects
        {
            get
            {
                return selects;
            }
            set
            {
                selects = value;
            }
        }



        private ConditionExpressionCollection wheres = new ConditionExpressionCollection();
        /// <summary>
        /// 属性条件集合
        /// </summary> 
        [DataMember]
        public ConditionExpressionCollection Wheres
        {
            get
            {
                return wheres;
            }
            set
            {
                wheres = value;
            }
        }




        private OrderExpressionCollection orderBys = new OrderExpressionCollection();
        /// <summary>
        /// 排序属性的集合
        /// </summary>  
        [DataMember]
        public OrderExpressionCollection OrderBys
        {
            get
            {
                return orderBys;
            }
            set
            {
                orderBys = value;
            }
        }



        private GroupExpressionCollection groupBys = new GroupExpressionCollection();
        /// <summary>
        /// 输出的属性集合
        /// </summary> 
        [DataMember]
        public GroupExpressionCollection GroupBys
        {
            get
            {
                return groupBys;
            }
            set
            {
                groupBys = value;
            }
        }


        private ConditionExpressionCollection havings = new ConditionExpressionCollection();
        /// <summary>
        /// 属性条件集合
        /// </summary> 
        [DataMember]
        public ConditionExpressionCollection Havings
        {
            get
            {
                return havings;
            }
            set
            {
                havings = value;
            }
        }

        #endregion



        #region 快速代码
        public QueryExpression Select(params object[] propertys)
        {
            Selects.Add(propertys);
            return this;
        } 
        public QueryExpression OrderBy(params IOrderExpression[] orderExpressions)
        {
            foreach (IOrderExpression orderExpression in orderExpressions)
            {
                OrderBys.Add(orderExpression);
            }
            return this;
        }
        public QueryExpression Where(ICondition conditionExpression)
        {
            wheres.Add(conditionExpression);
            return this;
        }
        public QueryExpression Having(ICondition conditionExpression)
        {
            havings.Add(conditionExpression);
            return this;
        }
        public QueryExpression GroupBy(params IPropertyExpression[] propertys)
        {
            foreach (IPropertyExpression property in propertys)
            {
                GroupBys.Add(new GroupExpression(property.PropertyName));
            }
            return this;
        }
        public QueryExpression GroupBy(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                GroupBys.Add(propertyName);
            }
            return this;
        }
        #endregion



        #region 显式接口
        IOutputExpressionCollection IQueryExpression.Selects
        {
            get
            {
                return selects;
            }
            set
            {
                selects = (OutputExpressionCollection)value;
            }
        }
        IConditionExpressionCollection IQueryExpression.Wheres
        {
            get
            {
                return wheres;
            }
            set
            {
                wheres = (ConditionExpressionCollection)value;
            }
        }

        IOrderExpressionCollection IQueryExpression.OrderBys
        {
            get
            {
                return orderBys;
            }
            set
            {
                orderBys = (OrderExpressionCollection)value;
            }
        }


        IGroupExpressionCollection IQueryExpression.GroupBys
        {
            get
            {
                return groupBys;
            }
            set
            {
                groupBys = (GroupExpressionCollection)value;
            }
        }

        IConditionExpressionCollection IQueryExpression.Havings
        {
            get
            {
                return havings;
            }
            set
            {
                havings = (ConditionExpressionCollection)value;
            }
        }
        #endregion

    }
}
