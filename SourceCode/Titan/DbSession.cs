using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using Titan.SqlTracer;
using System.Linq;

namespace Titan
{
    /// <summary>
    /// 数据库连接，实现IDbSession接口
    /// </summary>
    public class DbSession : DbHelper, IDbSession
    {



        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlProvider">需要传入类型，考虑到动态生成DbSession，不能用泛型</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlTracers">可以指定多个sql语句跟踪器，如果为null则不会有任何追踪</param>
        public DbSession(ISqlProvider sqlProvider, string connectionString, IObjectAccessor objectAccessor, IMappingProvider mappingProvider, ISqlTracer[] sqlTracers)
            : base(sqlProvider, connectionString, sqlTracers)
        {
            ObjectAccessor = objectAccessor;
            MappingProvider = mappingProvider;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlProvider">需要传入类型，考虑到动态生成DbSession，不能用泛型</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlTracers">可以指定多个sql语句跟踪器，如果为null则不会有任何追踪</param>
        public DbSession(ISqlProvider sqlProvider, string connectionString, ISqlTracer[] sqlTracers)
            : base(sqlProvider, connectionString, sqlTracers)
        {
            ObjectAccessor = EmitObjectAccessor.ObjectAccessor.Instance;
            MappingProvider = sqlProvider.CreateDefaultMappingProvider();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlProvider"></param>
        /// <param name="connectionString">数据库连接字符串</param>
        public DbSession(ISqlProvider sqlProvider, string connectionString)
            : this(sqlProvider, connectionString, null)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>  
        public DbSession(ISqlProvider sqlProvider)
            : this(sqlProvider, null, null)
        {
        }
        #endregion



        public IObjectAccessor ObjectAccessor { get; private set; }
        public IMappingProvider MappingProvider { get; private set; }

 
        #region 接口方法 ORM对象操作



        #region Insert
        public int Insert(object entity)
        {
            return InsertExclude(entity);
        }
        public int InsertInclude(object entity, params object[] includeProperties)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            IDictionary<string, object> nameValues = ObjectAccessor.ToPropertySets(MappingProvider, entityType, entity, Util.CollectNames(includeProperties));
            Dictionary<string, object> returns = new Dictionary<string, object>();
            int rtn = SqlProvider.Insert(this, MappingProvider, null, entityType, nameValues, returns);
            foreach (string propertyName in returns.Keys)
            {
                ObjectAccessor.Set(entity, propertyName, returns[propertyName]);
            }
            return rtn;
        }

        /// <summary>
        /// 将对象转化成insert语句，往数据库添加一条记录
        /// </summary>
        /// <param name="entity">待添加的对象</param>
        /// <param name="excludeProperties">生成sql语句时，强制不生成该参数指定的列</param> 
        /// <returns>数据库中返回的影响行数</returns>
        public int InsertExclude(object entity, params object[] excludeProperties)
        {
            //if (obj == null) throw ExceptionFactory.EntityNullException("obj");

            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            ITable table = MappingProvider.GetTable(entityType);
            IEnumerable<string> insertPropertyNames = (excludeProperties == null || excludeProperties.Length==0) ? table.InsertProperties : table.InsertProperties.Except(Util.CollectNames(excludeProperties), StringComparer.OrdinalIgnoreCase);

            IDictionary<string, object> nameValues = ObjectAccessor.ToPropertySets(MappingProvider, entityType, entity, insertPropertyNames);
            Dictionary<string, object> returns = new Dictionary<string, object>();
            int rtn = SqlProvider.Insert(this, MappingProvider, null, entityType, nameValues, returns);
            foreach (string propertyName in returns.Keys)
            {
                ObjectAccessor.Set(entity, propertyName, returns[propertyName]);
            }
            return rtn;
        }
        #endregion

        #region Update
        public int Update(object entity, params object[] updateProperties)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);

            ITable table = MappingProvider.GetTable(entityType);
            IDictionary<string, object> nameValues;
            if (updateProperties == null || updateProperties.Length==0)
            {
                //说明是更新所有列
                nameValues = ObjectAccessor.ToPropertySets(MappingProvider, entityType, entity, Util.CollectNames(table.NonPrimaryKeyProperties));
            }
            else
            {
                //说明是更新指定列
                nameValues = ObjectAccessor.ToPropertySets(MappingProvider, entityType, entity, Util.CollectNames(updateProperties), table.PrimaryProperties);
            }
            if (nameValues.Count <= 0)
            {
                return 0;
            }
            ConditionExpressionCollection cs = CreateConditionByKeyColumn(entity);

            return SqlProvider.Update(this, MappingProvider, null, entityType, cs, nameValues);
        }
        #endregion

        #region Delete

        public int Delete(object entity)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            ConditionExpressionCollection cs = CreateConditionByKeyColumn(entity);
            return SqlProvider.Delete(this, MappingProvider, null, entityType, cs);
        }
        #endregion

        #region BatchUpdate
        public int BatchUpdate(object entity, ICondition condition, params object[] updateProperties)
        {
            //if (obj == null) throw ExceptionFactory.EntityNullException("obj");
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);

            

            IDictionary<string, object> nameValues = ObjectAccessor.ToPropertySets(MappingProvider, entityType, entity, Util.CollectNames(updateProperties));
            return SqlProvider.Update(this, MappingProvider, null, entityType, condition, nameValues);
        }

        #endregion

        #region BatchDelete
        public int BatchDelete<T>(ICondition condition)
        {
            object entityType = typeof(T);
            return BatchDelete(entityType, condition);
        }
        public int BatchDelete(object entityType, ICondition condition)
        {
            return SqlProvider.Delete(this, MappingProvider, null, entityType, condition);
        }
        #endregion

        #region Select
        public bool Select(object entity)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            ITable table = MappingProvider.GetTable(entityType);
            return Select(entity, table.NonPrimaryKeyProperties);
        }
        public bool Select(object entity, params object[] outputProperties)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            List<string> outputs = Util.CollectNames(outputProperties);

            QueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            if (outputProperties == null || outputProperties.Length == 0)
            {
                ITable table = MappingProvider.GetTable(entityType); 
                queryExpression.Selects.Add(table.NonPrimaryKeyProperties);
            }
            else
            {
                queryExpression.Selects.Add(outputs);
            }
            queryExpression.Wheres = CreateConditionByKeyColumn(entity);

            if (queryExpression.Selects.Count <= 0)
            {
                return Exists(entity);
            }
            SelectionResult result = SqlProvider.SelectCollection(this, MappingProvider, null, queryExpression);

            bool hasResult = result.DataReader.Read();
            if (hasResult)
            {
                ObjectAccessor.Fill(SqlProvider,MappingProvider, result.DataReader, entity, result.ObjectFiller);
            }
            result.DataReader.Close();

            return hasResult;
        }
        #endregion

        #region SelectOne
        public object SelectOne(IQueryExpression queryExpression)
        {
            IList list = new List<object>(1);
            IQueryExpression q2 = new QueryExpression(); 
            q2.EntityType = queryExpression.EntityType;
            q2.GroupBys = queryExpression.GroupBys;
            q2.Havings = queryExpression.Havings;
            q2.IsDistinct = queryExpression.IsDistinct;
            q2.OrderBys = queryExpression.OrderBys;
            q2.PageIndex = 1;
            q2.PageSize = 1;
            q2.ReturnMatchedCount = false;
            q2.Selects = queryExpression.Selects;
            q2.Wheres = queryExpression.Wheres; 
            SelectCollection(list, q2);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SelectCollection
        public int SelectCollection(IList list, IQueryExpression queryExpression)
        {

            if (queryExpression.EntityType == null) throw new InvalidOperationException("queryExpression.EntityType属性不允许空");
            SelectionResult result = SqlProvider.SelectCollection(this, MappingProvider, null, queryExpression);

            int oldCount = list.Count;
            while (result.DataReader.Read())
            {
                object entity = ObjectAccessor.CreateInstance(queryExpression.EntityType);
                ObjectAccessor.Fill(SqlProvider,MappingProvider, result.DataReader, entity, result.ObjectFiller);
                list.Add(entity);
            }

            result.DataReader.Close();
            int i = queryExpression.ReturnMatchedCount ? result.TotalMatchedCount : list.Count - oldCount;
            return i;

        }
        #endregion

        #region
        public int SelectCount<T>(ICondition condition)
        {
            object entityType = typeof(T);
            return SqlProvider.SelectCount(this, MappingProvider, null, entityType, condition);
        }
        public int SelectCount(object entityType, ICondition condition)
        {
            return SqlProvider.SelectCount(this, MappingProvider, null, entityType, condition);
        }


        #endregion

        #region Exists

        public bool Exists(object entity)
        {
            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            ICondition condition = CreateConditionByKeyColumn(entity);
            return Exists(entityType, condition);
        }
        public bool Exists<T>(ICondition condition)
        {
            return Exists(typeof(T),condition);
        }
        public bool Exists(object entityType, ICondition condition)
        {
            return SqlProvider.Exists(this, MappingProvider, null, entityType, condition);
        }




        #endregion

        //public void ExecuteReaderAndFill(object obj, IList[] lists)
        //{
        //    this.ExecuteReaderAndFill(obj, lists, null);
        //}

        //public void ExecuteReaderAndFill(object obj, IList[] lists, EntityType[] entityTypes)
        //{
        //    if (obj == null) throw ExceptionFactory.EntityNullException("obj");
        //    int i = 0;
        //    foreach (IList list in lists)
        //    {
        //        if (list == null) throw ExceptionFactory.EntityNullException("lists[" + i + "]");
        //        if (!list.GetType().IsGenericType)
        //        {
        //            throw new ArgumentException("必须是泛型接口");
        //        }
        //        i++;
        //    }
        //    SqlProvider.ExecuteReaderAndFill(this, obj, lists, entityTypes);
        //}
        public IDataReader ExecuteReader(object command)
        {


            object commandType = MappingProvider.GetCommandTypeByInstance(command);
            IStatement statement = MappingProvider.GetStatement(commandType);
            IDictionary<string, object> nameValues = ObjectAccessor.ToPropertySets(MappingProvider, commandType, command, Util.CollectNames(statement.Parameters.Keys));
            IDataReader reader = SqlProvider.ExecuteReader(this, MappingProvider, commandType, nameValues);
            //fill
            foreach (KeyValuePair<string, IParameter> kv in statement.OutParameters)
            {
                ObjectAccessor.Set(command, kv.Key, nameValues[kv.Key]);
            }
            return reader;

        }
        public int ExecuteNonQuery(object command)
        {
            object commandType = MappingProvider.GetCommandTypeByInstance(command);
            IStatement statement = MappingProvider.GetStatement(commandType);
            IDictionary<string, object> nameValues = ObjectAccessor.ToPropertySets(MappingProvider, commandType, command, Util.CollectNames(statement.Parameters.Keys));
            int i = SqlProvider.ExecuteNonQuery(this, MappingProvider, commandType, nameValues);
            //fill
            foreach (KeyValuePair<string, IParameter> kv in statement.OutParameters)
            {
                ObjectAccessor.Set(command, kv.Key, nameValues[kv.Key]);
            }
            return i;
        }

        #endregion


        #region helper


        private ConditionExpressionCollection CreateConditionByKeyColumn(object entity)
        {
            //if (obj == null) throw ExceptionFactory.EntityNullException("obj");
            ConditionExpressionCollection cs = new ConditionExpressionCollection();


            object entityType = MappingProvider.GetEntityTypeByInstance(entity);
            var table = MappingProvider.GetTable(entityType);
            foreach (string property in table.PrimaryProperties)
            {
                cs.Add(new ConditionExpression(property, ConditionOperator.Equal, ObjectAccessor.Get(entity, property)));
            }
            return cs;
        }

        

        #endregion

    }
}
