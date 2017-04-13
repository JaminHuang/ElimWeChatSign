using System;


namespace Titan
{
    /// <summary>
    /// 描述命令类属性的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ParameterAttributeBase:Attribute
    { 




        /// <summary>
        /// 数据库中参数名称，如果为空则默认与属性名称一致。
        /// 建议这个属性设置为空
        /// </summary>
        public string ParameterName { get; set; }



        private int size = -1;
        /// <summary>
        /// 参数长度
        /// </summary>
        public int Size { get { return size; } set { size = value; } }
         
         

 
    }
}
