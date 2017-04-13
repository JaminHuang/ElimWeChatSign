using System.Data;
using System.Collections;

namespace Titan
{
    /// <summary>
    /// 操作数据库的类，包含一组常用的Helper方法和一组ORM对象操作的方法。内部承载了一个IDbConnection数据库连接对象
    /// </summary>
    public interface IDbSession : IDbHelper
    {

        IObjectAccessor ObjectAccessor { get; }
        IMappingProvider MappingProvider { get; }


        /// <summary>
        /// 执行一条Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns>影响行数</returns>
        new int ExecuteNonQuery(string sql);
        /// <summary>
        /// 执行Command
        /// </summary>
        /// <param name="command">IDbCommand对象</param>
        /// <returns>影响行数</returns>
        new int ExecuteNonQuery(IDbCommand command);


        /// <summary>
        /// 获取DataReader
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        new IDataReader ExecuteReader(string sql);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        new IDataReader ExecuteReader(IDbCommand command);

        #region ORM对象操作

        


        #region Insert

        /// <summary>
        /// 将对象转化成insert语句，往数据库添加一条记录
        /// </summary>
        /// <param name="entity">待添加的对象</param>
        /// <returns>数据库中返回的影响行数</returns>
        int Insert(object entity);



        /// <summary>
        /// 将对象转化成insert语句，往数据库添加一条记录
        /// </summary>
        /// <param name="entity">待添加的对象</param>
        /// <param name="includeProperties">生成sql语句时，强制不生成该参数指定的列</param> 
        /// <returns>数据库中返回的影响行数</returns>
        int InsertInclude(object entity, params object[] includeProperties);

        /// <summary>
        /// 将对象转化成insert语句，往数据库添加一条记录
        /// </summary>
        /// <param name="entity">待添加的对象</param>
        /// <param name="excludeProperties">生成sql语句时，强制不生成该参数指定的列</param> 
        /// <returns>数据库中返回的影响行数</returns>
        int InsertExclude(object entity, params object[] excludeProperties);

         
         

        #endregion



        #region Update 

        /// <summary>
        /// 只修改数据库中指定的列，将对象转化为update语句，修改数据库中的一条记录。注意设置传入对象的主键值
        /// </summary>
        /// <param name="entity">实体对象的实例，注意设置对象的主键值</param>
        /// <param name="updateProperties">需要修改的列</param>
        /// <returns>数据库中返回的影响行数</returns>
        int Update(object entity, params object[] updateProperties); 

         


        #endregion


        #region Delete


        /// <summary>
        /// 将对象转化成delete语句，从数据库删除一条记录，注意设置对象主键值
        /// </summary>
        /// <param name="entity">实体对象的实例</param>
        /// <returns>数据库中返回的影响行数</returns>
        int Delete(object entity);
         


        #endregion

        #region BatchUpdate 
        /// <summary>
        /// 根据指定条件批量更新记录，将所有符合指定条件的记录的指定列全部更新为指定对象的相应值
        /// </summary>
        /// <param name="entity">需要更新的属性值</param>
        /// <param name="condition">更新条件</param>
        /// <param name="updateProperties">需要更新的列</param>
        /// <returns>数据库中返回的影响行数</returns>
        int BatchUpdate(object entity, ICondition condition, params object[] updateProperties); 

         

        #endregion


        #region BatchDelete
        /// <summary>
        /// 从数据库中批量删除数据
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="condition">删除的条件</param>
        /// <returns></returns>
        int BatchDelete<T>(ICondition condition);
        int BatchDelete(object entityType, ICondition condition);   

        #endregion

        #region Select
        /// <summary>
        /// 根据主键从数据库获取对象，以提高性能
        /// </summary>
        /// <param name="entity">实体对象的实例，注意设置对象的主键值</param> 
        /// <returns>是否找到记录</returns>
        bool Select(object entity); 

        /// <summary>
        /// 根据主键从数据库获取对象指定列的值，以提高性能
        /// </summary>
        /// <param name="entity">实体对象的实例，注意设置对象的主键值</param>
        /// <param name="outputPropertyNames">需要输出的列</param>
        /// <returns>是否找到记录</returns>
        bool Select(object entity, params object[] outputPropertyNames);

        #endregion

        #region SelectOne 
        /// <summary>
        /// 查找符合条件的第一个对象，返回null表示没有找到
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <returns></returns>
        object SelectOne(IQueryExpression queryExpression);
        #endregion


        #region SelectCollection 



        int SelectCollection(IList list, IQueryExpression queryExpression);

        #endregion

        #region SelectCount
        /// <summary>
        /// 获取符合条件的记录数
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="condition">条件</param>
        /// <returns>符合条件的记录数</returns>
        int SelectCount<T>(ICondition condition);
        int SelectCount(object entityType, ICondition condition);  


 

        #endregion

        #region Exists


        bool Exists<T>(ICondition condition);
        bool Exists(object entityType, ICondition condition);

        /// <summary>
        /// 对象是否存在
        /// </summary>
        /// <param name="entity">注意设置主键</param>
        /// <returns></returns>
        bool Exists(object entity);

          

        #endregion

        #region Command
        ///// <summary>
        ///// 执行命令实体，并将结果集填充到集合对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="command"></param>
        ///// <param name="lists"></param>
        ///// <returns>command中返回的数据库影响行数</returns>
        //void ExecuteReaderAndFill(object command, IList[] lists);
        ///// <summary>
        ///// 执行命令实体，并将结果集填充到集合对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="command"></param>
        ///// <param name="lists"></param>
        ///// <param name="entityTypes"></param>
        ///// <returns>command中返回的数据库影响行数</returns>
        //void ExecuteReaderAndFill(object command, IList[] lists, object[] entityTypes);

         
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/ms971497, you must close the datareader before you process the output parameters
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(object command);

        /// <summary>
        /// 执行命令实体类
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        int ExecuteNonQuery(object command);
        #endregion

        #endregion
    }
}
