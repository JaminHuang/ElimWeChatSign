using System.Data;
using System;

namespace Titan
{
    public interface IParameter
    { 
        /// <summary>
        /// 
        /// </summary>
        string ParameterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// 参数方向，只允许在StatementParameterBase中赋值，以保护不被其它Attribute覆盖
        /// </summary>
        ParameterDirection Direction { get; set; } 
         
        ///// <summary>
        ///// 属性代理，有时Column标注在Property上，有时可能标注在Field上，通过这个代理对象可以方便获取或者设置对象属性
        ///// </summary>
        //PropertyAdapter PropertyAdapter { get; set; }

        Type PropertyType { get; set; }


    }
}
