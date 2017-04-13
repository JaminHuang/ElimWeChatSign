 

namespace Titan
{
    /// <summary>
    /// 告诉系统应如何在生成insert,update语句时使用何种规则
    /// </summary>
    public class ColumnSqlBehavior
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnSqlBehavior()
        {
            Generate = true;
            ValueBehavior = ValueBehavior.UsePropertyValue;
        }
        
        /// <summary>
        /// 在生成insert,update语句时是否包含本字段
        /// 假设有个实体类MyClass有三个字段F1,F2,F3，其中F2的Generate属性为flase，
        /// 那么生成insert语句是：insert into MyClass(F1,F3) values(@F1,@F3)，注意这句insert语句中是没有F2字段的。
        /// </summary>
        public bool Generate { get; set; }


        /// <summary>
        /// 生成sql语句中的value段时使用何种行为，只有Generate属性为true时本属性才有效。
        /// 假设有个实体类MyClass有三个字段F1,F2,F3，其中F2的Generate属性为true且ValueBehavior为UsePropertyValue，
        /// 那么生成insert语句是：insert into MyClass(F1,F2,F3) values(@F1,@F2,@F3)；
        /// 如果ValueBehavior为UseValueExpression，
        /// 那么生成insert语句是：insert into MyClass(F1,F2,F3) values(@F1,{},@F3)；
        /// </summary>
        public ValueBehavior ValueBehavior { get; set; }



        /// <summary>
        /// 生成sql语句中的value段时使用的表达式，只有Generate属性为true且ValueBehavior属性为UseValueExpression时本属性才有效。
        /// </summary>
        public string ValueExpression { get; set; }
    }
}
