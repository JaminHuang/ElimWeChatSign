using Titan;
using Titan.SqlServer;
using Titan.SqlTracer;

namespace ElimWeChatSign.Service
{
    /// <summary>
    /// 基本服务配置
    /// </summary>
    public class ServerBase
    {
        /// <summary>
        /// SqlLog语句日志地址
        /// </summary>
        private static string SqlLogFile = "d:\\sqlLog\\{yyyyMMdd}.txt";
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string ConnectionString = @"Data Source=bds24547528.my3w.com;Initial Catalog=bds24547528_db;User Id=bds24547528;Password=family2016;";
        /// <summary>
        /// 数据库类型
        /// </summary>
        private static ISqlProvider SqlProvider = new SqlServerSqlProvider();
        /// <summary>
        /// SqlLog语句配置
        /// </summary>
		private static ISqlTracer[] SqlTracers = new ISqlTracer[] { };
        //private static ISqlTracer[] SqlTracers = new ISqlTracer[] { new FileSqlTracer { FileName = SqlLogFile } };
        /// <summary>
        /// 配置Session
        /// </summary>
        public static IDbSession Session = OpenSession();
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbSession OpenSession()
        {
            IDbSession session = new DbSession(SqlProvider, ConnectionString, SqlTracers);
            session.Open();
            return session;
        }
    }
}
