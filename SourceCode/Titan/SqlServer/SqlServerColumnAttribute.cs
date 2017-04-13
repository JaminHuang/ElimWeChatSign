namespace Titan.SqlServer
{
    /// <summary>
    /// 标注在类的属性或字段上，用以描述针对SqlServer特有的数据库列(Column)信息。
    /// 同样一个实体类的属性或字段，针对不同的数据库有不同的sql语句生成规则。通过本类来标注一些SqlServer特有的数据库列(Column)信息。
    /// 注意：使用这个标注前提是必须同时有ColumnAttribute。
    /// </summary>  
    public class SqlServerColumnAttribute : ColumnWithIdentityAttribute
    {  


        /// <summary>
        /// SqlServer特有的特性，如果为true,会用like代替=
        /// </summary>
        public bool IsNText { get; set; }
    }
}
