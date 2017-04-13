using System;
using System.Data;
using System.Data.Common;
using Titan.SqlTracer;

namespace Titan
{
    /// <summary>
    /// 数据库连接，实现IDbSession接口
    /// </summary>
    public class DbHelper : IDbHelper
    { 

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlProvider">需要传入类型，考虑到动态生成DbSession，不能用泛型</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlTracers">可以指定多个sql语句跟踪器，如果为null则不会有任何追踪</param>
        public DbHelper(ISqlProvider sqlProvider, string connectionString, ISqlTracer[] sqlTracers)
        {
            if (sqlProvider == null)
            {
                throw new ArgumentNullException("sqlProvider");
            }
            SqlProvider = sqlProvider;
            Connection = SqlProvider.CreateConnection();
            Connection.ConnectionString = connectionString;
            SqlTracers = sqlTracers;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlProvider"></param>
        /// <param name="connectionString">数据库连接字符串</param>
        public DbHelper(ISqlProvider sqlProvider, string connectionString)
            : this(sqlProvider, connectionString, null)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>  
        public DbHelper(ISqlProvider sqlProvider)
            : this(sqlProvider, null, null)
        {
        }
        #endregion

        #region 接口属性


        public virtual ISqlProvider SqlProvider { get; private set; }

        public virtual ISqlTracer[] SqlTracers { get; private set; }

        public virtual IDbConnection Connection { get; private set; }


        public virtual IDbTransaction Transaction { get; private set; } 

        #endregion

        #region 接口方法

        public virtual void Open()
        {
            Connection.Open();
        }


        public virtual void Close()
        {
            Transaction = null;
            Connection.Close();
        }


        public virtual void BeginTransaction()
        {
            if (Connection == null) throw ConnectionNotOpendException();
            if (Transaction != null) throw TransactionOpendException();
            Transaction = Connection.BeginTransaction();
        }


        public virtual void BeginTransaction(IsolationLevel iso)
        {
            if (Connection == null) throw ConnectionNotOpendException();
            if (Transaction != null) throw TransactionOpendException();
            Transaction = Connection.BeginTransaction(iso);
        }




        public virtual void Commit()
        {
            if (Transaction == null) throw TransactionNotOpendException();
            Transaction.Commit();
            Transaction = null;
        }


        public virtual void Rollback()
        {
            if (Transaction == null) throw TransactionNotOpendException();
            Transaction.Rollback();
            Transaction = null;
        }
        #endregion

        #region 接口方法 常规helper


        public virtual DataRow ExecuteFirstDataRow(IDbCommand command)
        {
            DataRow dataRow = null;
            PrepareCommand(command);
            IDbDataAdapter ad = SqlProvider.CreateDataAdapter();
            ad.SelectCommand = command;
            var ad2 = ad as DbDataAdapter;
            var dt = new DataTable();
            ad2.Fill(0, 1, dt);

            if (dt.Rows.Count > 0)
            {
                dataRow = dt.Rows[0];
            }

            return dataRow;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteDataTable(IDbCommand command)
        {
            PrepareCommand(command);
            IDbDataAdapter ad = SqlProvider.CreateDataAdapter();
            ad.SelectCommand = command;
            var ad2 = ad as DbDataAdapter;
            var dt = new DataTable();
            ad2.Fill(dt);

            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(IDbCommand command, int pageSize, int pageIndex)
        {
            SqlProvider.WrapPaging(this, command, pageSize, pageIndex);
            return ExecuteDataTable(command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbCommand command, int pageSize, int pageIndex)
        {
            SqlProvider.WrapPaging(this, command, pageSize, pageIndex);
            return ExecuteReader(command);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual bool RecordExists(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return RecordExists(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual bool RecordExists(IDbCommand command)
        {
            PrepareCommand(command);
            object obj = command.ExecuteScalar();
            return obj != null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataRow ExecuteFirstDataRow(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteFirstDataRow(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteDataTable(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteDataTable(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteDataSet(command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(IDbCommand command)
        {
            PrepareCommand(command);
            IDbDataAdapter ad = SqlProvider.CreateDataAdapter();
            ad.SelectCommand = command;
            var ds = new DataSet();
            ad.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual IDataReader ExecuteReader(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteReader(command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public virtual IDataReader ExecuteReader(IDbCommand command, CommandBehavior behavior)
        {
            PrepareCommand(command);
            return command.ExecuteReader(behavior);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual IDataReader ExecuteReader(IDbCommand command)
        {
            PrepareCommand(command);
            return command.ExecuteReader();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteNonQuery(command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(IDbCommand command)
        {
            PrepareCommand(command);
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql)
        {
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            return ExecuteScalar(command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(IDbCommand command)
        {
            PrepareCommand(command);
            return command.ExecuteScalar();
        }
        #endregion




        public void Dispose()
        {
            Close();
        }






        private void PrepareCommand(IDbCommand command)
        {
            if (command.Connection == null)
            {
                if (Connection == null) throw ConnectionNotOpendException();
                command.Connection = Connection;
            }
            if (command.Transaction == null)
            {
                if (Transaction != null && Transaction.Connection != null)
                {
                    command.Transaction = Transaction;
                }
            }

            //trace sql
            if (SqlTracers == null) return;
            foreach (ISqlTracer sqlTracer in SqlTracers)
            {
                sqlTracer.Trace(command);
            }
        }

        private InvalidOperationException ConnectionNotOpendException()
        {
            return new InvalidOperationException("数据库连接未打开，请先使用Open()方法打开数据库连接");
        }
        private InvalidOperationException TransactionOpendException()
        {
            return new InvalidOperationException("事务已打开，在提交前不能重复打开事务");
        }
        private InvalidOperationException TransactionNotOpendException()
        {
            return new InvalidOperationException("事务未打开，请先使用BeginTransaction()方法启动一个数据库事务");
        }

    }
}
