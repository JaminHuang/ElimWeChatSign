using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Titan
{
    /// <summary>
    /// sql解析器，根据对象生成sql语句,将查询结果赋值给对象等
    /// </summary>
    public interface ISqlProvider
    {
         
         
        

        /// <summary>
        /// 创建一个数据库连接实例，如针对oracle应当返回OracleConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();


        /// <summary>
        /// 创建一个DataAdapter实例，例如针对oracle应当返回OracleDataAdapter
        /// </summary>
        /// <returns></returns>
        DbDataAdapter CreateDataAdapter();


        /// <summary>
        /// 对传入的command对象的CommandText包装一层分页sql，例如针对oracle数据库拼接的sql中可能包含rownum
        /// </summary>
        /// <param name="db"></param>
        /// <param name="command"></param>
        /// <param name="pageSize">每页记录数，必须是>=1</param>
        /// <param name="pageIndex">从1开始的页码，必须是>=1</param>
        /// <returns></returns>
        void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex);


        /// <summary>
        /// 将查询结果集dataReader中的数据填充到实体对象的某个属性
        /// </summary>
        /// <param name="value">datareader中取回的值</param> 
        /// <param name="expectedType">预期返回的类型</param> 
        object ConvertDbValue(object value, Type expectedType);


        /// <summary>
        /// 根据obj对象的映射信息(ITable)，生成insert语句并执行。注意必须传入实体类，因为需要承载数据库返回值
        /// </summary>
        /// <param name="db"></param> 
        /// <param name="tableNameMapping"></param>
        /// <param name="entityType"></param> 
        /// <param name="inserts">需要生成insert的属性列表</param>
        /// <param name="mappingProvider"></param>
        /// <param name="returns">如果有数据库返回值，则会存储到这个对象中</param>
        /// <returns></returns>
        int Insert(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableNameMapping, object entityType, IDictionary<string, object> inserts, IDictionary<string, object> returns);


        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entityType"></param>
        /// <param name="condition"></param> 
        /// <param name="updates"></param>
        /// <param name="mappingProvider"></param>
        /// <param name="tableNameMapping"></param>
        /// <returns></returns>
        int Update(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition, IDictionary<string, object> updates);


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="db"></param> 
        /// <param name="mappingProvider"></param>
        /// <param name="tableNameMapping"></param>
        /// <param name="entityType"></param> 
        /// <param name="condition"></param>
        /// <returns></returns>
        int Delete(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>   
        /// <param name="mappingProvider"></param>
        /// <param name="tableNameMapping"></param>
        /// <param name="queryExpression"></param>
        /// <returns>如果在queryExpression指定了ReturnMatchedCount为true，则返回全部匹配数，否则返回实际读到的个数</returns>
        SelectionResult SelectCollection(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping,IQueryExpression queryExpression);



        int SelectCount(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);


        /// <summary>
        /// 考虑性能问题，如果使用selectCount则数据库扫描数据量比较大
        /// </summary>
        /// <param name="db"></param> 
        /// <param name="mappingProvider"></param>
        /// <param name="tableNameMapping"></param>
        /// <param name="entityType"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        bool Exists(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);






        IDataReader ExecuteReader(IDbHelper db, IMappingProvider mappingProvider, object commandType, IDictionary<string, object> values);

        int ExecuteNonQuery(IDbHelper db, IMappingProvider mappingProvider, object commandType, IDictionary<string, object> values);



        IMappingProvider CreateDefaultMappingProvider();
        
    }
}
