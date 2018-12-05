using JaminHuang.Util;
using SharpConfig;
using System;
using Titan;
using Titan.MySql;
using Titan.SqlTracer;

namespace ElimWeChatSign.Service
{
    public class BaseService
    {
        private static Configuration cfx = ConfigHelper.GetInstance();

        /// <summary>
        /// SqlLog语句日志地址
        /// </summary>
        private static string SqlLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\sqlLog\\{yyyyMMdd}.txt";
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string ConnectionString = CryptographyUtil.AESDecryptServer(cfx["mysql"]["connection"].StringValue);
        /// <summary>
        /// 数据库类型
        /// </summary>
        private static ISqlProvider SqlProvider = new MySqlSqlProvider();
        /// <summary>
        /// SqlLog语句配置
        /// </summary>
        private static ISqlTracer[] SqlTracers = new ISqlTracer[] { new FileSqlTracer { FileName = SqlLogFile } };
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
