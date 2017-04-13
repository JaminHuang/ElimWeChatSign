using System.Data; 

namespace Titan.SqlTracer
{
    /// <summary>
    /// sql语句跟踪接口
    /// </summary>
    public interface ISqlTracer
    {
        /// <summary>
        /// 每次执行sql语句前都会调用这个方法，方便跟踪sql语句
        /// </summary>
        /// <param name="command">待执行的command</param>
        void Trace(IDbCommand command);


        ///// <summary>
        ///// 初始化，在使用配置文件的情况下，会将配置文件中的内容以键值对的方式传入
        ///// </summary>
        ///// <param name="parameters"></param>
        //void Initialize(IDictionary<string, string> parameters);
    }
}
