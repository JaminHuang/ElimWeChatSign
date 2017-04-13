namespace Titan
{
    /// <summary>
    /// 标注实体类属性或字段，用来描述对应数据库列的规则。
    /// 不同数据库会有不同的特性，例如同一个属性在不同的数据库中对应的列名可能不相同。本类中的属性均可覆盖。
    /// </summary>
    public sealed class ColumnAttribute : ColumnAttributeBase
    { 

        #region 不可覆盖成员，也即针对不同数据库有一致的行为

        /// <summary>
        /// 显示的友好名称，一般是用于校验时给出友好显示。
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        /// 是否为数据库中主键字段
        /// </summary>
        public bool IsPrimaryKey { get; set; }



        private AttributeBoolean returnAfterInsert = AttributeBoolean.Null;//注意必须是使用这种类型的，因为未标记的时候针对自动增长列有特别的含义
        /// <summary>
        /// 执行完insert语句后是否返回数据库中生成的值，一般应用于自动增长的列
        /// </summary>
        public AttributeBoolean ReturnAfterInsert { get { return returnAfterInsert; } set { returnAfterInsert = value; } }

        #endregion
 

    }
}
