namespace Titan.Oracle
{ 
    public class OracleColumnAttribute : ColumnAttributeBase
    {
 

        /// <summary>
        /// 标注本字段的值是oracle中序列生成的，指定序列的名称，生成insert的语句如：insert into table1 (f1,f2) values (sss.nextval,:v1)
        /// 是否要将值返回，则由ColumnAttribute的ReturnAfterInsert属性确定。
        /// 注意：设置了这个值后，会将DefaultValue覆盖成{SequenceName}.nextval！
        /// </summary>
        public string Sequence { get; set; }
    }
}
