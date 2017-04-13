using System;


namespace Titan.Oracle
{
    /// <summary>
    /// 描述命令类属性的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class OracleCursorAttribute : Attribute
    {
        /// <summary>
        /// 数据库中参数名称，如果为空则默认与属性名称一致。
        /// 注意不能加@/:等符号，这些符号是会自动生成的，
        /// 建议这个属性设置为空
        /// </summary>
        public string ParameterName{get;set;}

 

 

 
    }
}
