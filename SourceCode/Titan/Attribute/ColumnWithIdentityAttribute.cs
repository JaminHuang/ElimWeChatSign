using System;


namespace Titan
{
    /// <summary>
    /// 标注实体类属性或字段，用来描述对应数据库列的规则。
    /// 不同数据库会有不同的特性，例如同一个属性在不同的数据库中对应的列名可能不相同。本类中的属性均可覆盖。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ColumnWithIdentityAttribute : ColumnAttributeBase
    {

        /// <summary>
        /// 标注本字段的值是oracle中序列生成的，指定序列的名称，生成insert的语句如：insert into table1 (f1,f2) values (sss.nextval,:v1)
        /// 是否要将值返回，则由ColumnAttribute的ReturnAfterInsert属性确定。
        /// 注意：设置了这个值后，会将DefaultValue覆盖成{SequenceName}.nextval！
        /// </summary>
        //public string SequenceName { get; set; }
        public bool IsIdentity { get; set; }

         
    }
}
