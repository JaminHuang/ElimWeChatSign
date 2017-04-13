using System;

namespace Titan
{
    /// <summary>
    /// 描述命令实体类的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class StatementAttributeBase : Attribute
    {




        private AttributeCommandType commandType = AttributeCommandType.StoredProcedure;
        /// <summary>
        /// 命令类型，对应System.Data.IDbCommand.CommandType属性
        /// 针对不同数据库可能不同，应当在其它***StatementAttribute中覆盖
        /// </summary>
        public AttributeCommandType CommandType { get { return commandType; } set { commandType = value; } }





        /// <summary>
        /// 命令文本，对应System.Data.IDbCommand.CommandText属性
        /// 针对不同数据库可能不同，应当在其它***StatementAttribute中覆盖
        /// </summary>
        public string CommandText { get; set; }



    }
}
