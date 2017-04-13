using System;
using System.Data;
using Titan.SqlTracer;

namespace Titan
{
    /// <summary>
    /// 操作数据库的类，包含一组常用的Helper方法和一组ORM对象操作的方法。内部承载了一个IDbConnection数据库连接对象
    /// </summary>
    public interface IDbHelper : IDisposable
    {

        /// <summary>
        /// 获取内部承载的sql解析器，针对不同类型的数据库有不同的解析器
        /// </summary>
        ISqlProvider SqlProvider { get; }

        /// <summary>
        /// 获取sql语句跟踪器
        /// </summary>
        ISqlTracer[] SqlTracers { get; }

        /// <summary>
        /// 获取内部承载的数据库连接对象
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 获取执行BeginTransaction方法后产生的IDbTransaction对象
        /// </summary>
        IDbTransaction Transaction { get; }

         

        /// <summary>
        /// 打开数据库会话，会将内部承载的Connection对象打开
        /// </summary>
        void Open();
        /// <summary>
        /// 关闭数据库会话，会将内部承载的Connection对象关闭
        /// </summary>
        void Close();

        /// <summary>
        /// 开始一个数据库事务，调用后可以通过Transaction属性获取到事务实例
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 开始一个数据库事务，调用后可以通过Transaction属性获取到事务实例
        /// </summary>
        /// <param name="iso">指定连接事务的锁定行为</param>
        void BeginTransaction(IsolationLevel iso);
        /// <summary>
        /// 提交事务，提交后会自动关闭事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚事务，回滚完毕后会自动关闭事务
        /// </summary>
        void Rollback();





        #region 常规helper

        /// <summary>
        /// 判断记录是否存在 ,例如传入"select top 1 1 from t"
        /// </summary>
        /// <param name="sql">Sql语句</param> 
        /// <returns></returns>
        bool RecordExists(string sql);
        /// <summary>
        /// 判断记录是否存在 ,例如传入"select top 1 1 from t"
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool RecordExists(IDbCommand command);
        /// <summary>
        /// 获取第一行DataRow
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        DataRow ExecuteFirstDataRow(string sql);
        /// <summary>
        /// 获取第一行DataRow
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        DataRow ExecuteFirstDataRow(IDbCommand command);
        /// <summary>
        /// 获取一个DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql);
        /// <summary>
        /// 获取一个DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(IDbCommand command);
        /// <summary>
        /// 分页获取DataTable
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">第几页，从1开始</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(IDbCommand command, int pageSize, int pageIndex);
        /// <summary>
        /// 获取一个DataSet
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string sql);
        /// <summary>
        /// 获取一个DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(IDbCommand command);
        /// <summary>
        /// 获取DataReader
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        IDataReader ExecuteReader(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(IDbCommand command);
        /// <summary>
        /// 获取DataReader
        /// </summary>
        /// <param name="command"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(IDbCommand command, CommandBehavior behavior);
        /// <summary>
        /// 分页获取DataReader
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">第几页，从1开始</param>
        /// <returns></returns>
        IDataReader ExecuteReader(IDbCommand command, int pageSize, int pageIndex);
        /// <summary>
        /// 执行一条Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(string sql);
        /// <summary>
        /// 执行Command
        /// </summary>
        /// <param name="command">IDbCommand对象</param>
        /// <returns>影响行数</returns>
        int ExecuteNonQuery(IDbCommand command);
        /// <summary>
        /// 获取第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql);
        /// <summary>
        /// 获取第一行第一列的值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object ExecuteScalar(IDbCommand command);
        #endregion


    }
}
