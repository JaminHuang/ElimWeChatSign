namespace Titan.SqlServer
{
    public class SqlServerColumn : ColumnWithIdentity
    { 


        /// <summary>
        /// SqlServer特有，如果为true,会用like代替=
        /// </summary>
        public bool IsNText { get; set; }


         
    }
}
