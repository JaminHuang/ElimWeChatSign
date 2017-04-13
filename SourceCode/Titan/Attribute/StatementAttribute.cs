using System.Data;

namespace Titan
{
    /// <summary>
    /// 描述命令实体类的Attribute
    /// </summary> 
    public sealed class StatementAttribute : StatementAttributeBase
    {


        private CommandBehavior commandBehavior = CommandBehavior.Default;
        /// <summary>
        /// 针对不同的数据库应当有相同的行为，不可覆盖
        /// </summary>
        public CommandBehavior CommandBehavior { get { return commandBehavior; } set { commandBehavior = value; } }
         
         


    }
}
