using System.Collections.Generic; 
using System.Data; 

namespace Titan
{
    public interface IStatement
    { 
        /// <summary>
        /// 命令行为，只允许在StatementBase中赋值，以保护不被其它Attribute覆盖
        /// </summary>
        CommandBehavior CommandBehavior { get; set; }
        string CommandText { get; set; } 
        CommandType CommandType { get; set; }
        /// <summary>
        /// 以属性名作为主键
        /// </summary>
        Dictionary<string, IParameter> Parameters { get; set; }

        /// <summary>
        /// 缓存输出的参数
        /// </summary>
        Dictionary<string, IParameter> OutParameters { get; set; }

        void CreateCache();
    }
}
