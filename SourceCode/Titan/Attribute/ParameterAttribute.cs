using System.Data;


namespace Titan
{
    /// <summary>
    /// 描述命令类属性的Attribute
    /// </summary> 
    public sealed class ParameterAttribute : ParameterAttributeBase
    {

        private ParameterDirection direction = ParameterDirection.Input;
        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction { get { return direction; } set { direction = value; } }


    }
}
