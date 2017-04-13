using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Titan.ExpressionAnalyse;

namespace Titan
{
    public abstract class SqlProvider : ISqlProvider
    {

        #region 未实现的接口方法
        public abstract object ConvertDbValue(object value, Type expectedType);
        public abstract IDbConnection CreateConnection();
        public abstract DbDataAdapter CreateDataAdapter();
        public abstract void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex);
        public abstract int Insert(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableNameMapping, object entityType, IDictionary<string, object> inserts, IDictionary<string, object> returns);
        public abstract int Update(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition, IDictionary<string, object> updates);
        public abstract int Delete(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);
        public abstract SelectionResult SelectCollection(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, IQueryExpression queryExpression);
        public abstract int SelectCount(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);
        public abstract bool Exists(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object,string> tableNameMapping, object entityType, ICondition condition);
        public abstract IMappingProvider CreateDefaultMappingProvider();
        #endregion

        #region 已实现的接口方法
        public virtual IDataReader ExecuteReader(IDbHelper db, IMappingProvider mappingProvider, object commandType, IDictionary<string, object> values)
        { 
            IStatement statement = mappingProvider.GetStatement(commandType);

            IDbCommand dbCommand = CreateCommand(db, mappingProvider, commandType, values);
            IDataReader reader = db.ExecuteReader(dbCommand, statement.CommandBehavior);

            foreach (KeyValuePair<string, IParameter> kv in statement.OutParameters)
            {
                IParameter parameter=kv.Value;
                IDataParameter dataParameter = (IDataParameter)dbCommand.Parameters[parameter.ParameterName];
                object value=ConvertDbValue(dataParameter.Value, parameter.PropertyType);
                if (values.ContainsKey(kv.Key))
                {
                    values[kv.Key]=value;
                }
                else
                {
                    values.Add(kv.Key,value);
                } 
            }

            return reader;
        }

        public virtual int ExecuteNonQuery(IDbHelper db, IMappingProvider mappingProvider, object commandType, IDictionary<string, object> values)
        {
            IStatement statement = mappingProvider.GetStatement(commandType);

            IDbCommand dbCommand = CreateCommand(db, mappingProvider, commandType, values);
            int returnCount = db.ExecuteNonQuery(dbCommand);
            foreach (KeyValuePair<string, IParameter> kv in statement.OutParameters)
            {
                IParameter parameter = kv.Value;
                IDataParameter dataParameter = (IDataParameter)dbCommand.Parameters[parameter.ParameterName];
                object value = ConvertDbValue(dataParameter.Value, parameter.PropertyType);
                if (values.ContainsKey(kv.Key))
                {
                    values[kv.Key] = value;
                }
                else
                {
                    values.Add(kv.Key, value);
                }
            }
            return returnCount;
        }

        #endregion



        #region 子类可访问的方法






        #region 生成select相关的sql语句

        /// <summary>
        /// 生成一句完整的包含多表关联的 select from where order by 语句
        /// </summary>
        /// <param name="sb">拼接sql语句时会调用sb.Append方法</param>
        /// <param name="parameters">需要参数时，会生成创建参数并添加至本参数</param>
        /// <param name="parameterCounter">生成sql过程中参数计数器,添加parameters集合的参数的名称格式为{parameterNamePrefix}{parameterCounter}</param>  
        /// <param name="queryExpression">查询表达式</param>
        /// <param name="sqlAnalyseResult">查询表达式解析后的对象</param>
        /// <param name="mappingProvider"></param>
        /// <param name="tableNameMapping">动态表名称时使用</param>  
        protected virtual void GenerateSelectSql_SelectFromWhereOrder(
            StringBuilder sb,
            IDataParameterCollection parameters, ref int parameterCounter,
            IQueryExpression queryExpression, SqlAnalyseResult sqlAnalyseResult, IMappingProvider mappingProvider, IDictionary<object, string> tableNameMapping
            )
        {
            //select
            sb.Append("select ");
            int index = 0;
            foreach (string outputSqlColumn in sqlAnalyseResult.SortedOutputColumns)
            {
                if (index > 0) { sb.Append(","); }
                index++;
                sb.Append(outputSqlColumn);
            }


            //from where
            GenerateSelectSql_FromWhere(sb, parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider,tableNameMapping);



            //order
            if (queryExpression.OrderBys.Count > 0)
            {
                sb.Append(" order by ");

                index = 0;
                foreach (IOrderExpression orderExpression in queryExpression.OrderBys)
                {
                    if (index > 0) { sb.Append(","); }
                    index++;
                    SqlColumn sqlColumn = sqlAnalyseResult.SqlColumns[orderExpression.Property];
                    sb.Append(sqlColumn.TableAliasAndColumn);
                    if (orderExpression.OrderType == OrderType.Desc)
                    {
                        sb.Append(" desc ");
                    }
                }

            }
        }

        //生成包含关联表的select语句的 from where 部分
        protected virtual void GenerateSelectSql_FromWhere(
            StringBuilder sb,
            IDataParameterCollection parameters, ref int parameterCounter,
            IQueryExpression queryExpression, SqlAnalyseResult sqlAnalyseResult, IMappingProvider mappingProvider, IDictionary<object, string> tableNameMapping
            )
        {


            int index;


            //from
            sb.Append(" from ");
            sb.Append(GetTableName(sqlAnalyseResult.MasterEntityType,mappingProvider, tableNameMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);



            //join
            foreach (SqlTable sqlTable in sqlAnalyseResult.ForeignTables)
            {
                sb.Append(" left outer join ");
                sb.Append(GetTableName(sqlTable.ForeignEntityType,mappingProvider, tableNameMapping));
                sb.Append(" ");
                sb.Append(sqlTable.ForeignTableNameAlias);

                sb.Append(" on ");
                index = 0;
                foreach (SqlRelationColumn sqlRelationColumn in sqlTable.RelationColumns)
                {
                    if (index > 0) { sb.Append(" and "); }
                    index++;
                    sb.Append(sqlRelationColumn.Expression);

                }
            }

            //where 
            if (queryExpression.Wheres.Count > 0)
            {
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult, true, mappingProvider);
            }

            //group by 
            if (queryExpression.GroupBys.Count > 0)
            {
                sb.Append(" group by ");
                index = 0;
                foreach (IGroupExpression groupExpression in queryExpression.GroupBys)
                {
                    if (index > 0) { sb.Append(","); }
                    index++;
                    SqlColumn sqlColumn = sqlAnalyseResult.SqlColumns[new PropertyExpression(groupExpression.PropertyName, GroupFunction.None)];
                    sb.Append(sqlColumn.TableAliasAndColumn);
                }
            }

            //having
            if (queryExpression.Havings.Count > 0)
            {
                sb.Append(" having ");
                GenerateSelectSql_Where(sb, parameters, ref parameterCounter, queryExpression.Havings, sqlAnalyseResult, true, mappingProvider);
            }



        }


        //生成包含关联表的select语句的 where 部分
        protected virtual void GenerateSelectSql_Where(
            StringBuilder sb,
            IDataParameterCollection parameters, ref int parameterCounter,
            ICondition condition, SqlAnalyseResult sqlAnalyseResult, bool includeTableNameAlias, IMappingProvider mappingProvider
            )
        {
            if (condition is IConditionExpressionCollection)
            {
                IConditionExpressionCollection conditions = (IConditionExpressionCollection)condition;

                string andOr = conditions.ConditionRelation == ConditionRelation.And ? " and " : " or ";
                if (conditions.Count > 1) { sb.Append("("); }
                int index = 0;
                foreach (ICondition conditionItem in conditions)
                {
                    if (index > 0) { sb.Append(andOr); }
                    index++;
                    GenerateSelectSql_Where(sb, parameters, ref parameterCounter, conditionItem, sqlAnalyseResult, includeTableNameAlias, mappingProvider); 
                }
                if (conditions.Count > 1) { sb.Append(") "); }
            }
            else
            {
                IConditionExpression conditionItem = (IConditionExpression)condition;
                SqlColumn sqlColumn = sqlAnalyseResult.SqlColumns[conditionItem.Property];
                GenerateSelectSql_Where_Item(sb, parameters, ref parameterCounter, conditionItem, sqlColumn, includeTableNameAlias, mappingProvider); 
            }
        }


        protected virtual void GenerateSelectSql_Where_Item(
            StringBuilder sb,
            IDataParameterCollection parameters, ref int parameterCounter,
            IConditionExpression conditionExpression, SqlColumn sqlColumn, bool includeTableNameAlias, IMappingProvider mappingProvider
            )
        {

            //为处理Like自动增加%
            object objValue = conditionExpression.ConditionValue;
            switch (conditionExpression.ConditionOperator)
            {
                case ConditionOperator.Like:
                case ConditionOperator.NotLike:
                    objValue = "%" + objValue + "%";
                    break;
                case ConditionOperator.RightLike:
                case ConditionOperator.NotRightLike:
                    objValue = "%" + objValue;
                    break;
                case ConditionOperator.LeftLike:
                case ConditionOperator.NotLeftLike:
                    objValue = objValue + "%";
                    break;
            }



            bool hasParameter = false;
            //添加字段名
            sb.Append(includeTableNameAlias ? sqlColumn.TableAliasAndColumn : sqlColumn.Column.ColumnName);
            //添加操作符
            switch (conditionExpression.ConditionOperator)
            {
                case ConditionOperator.Equal:
                    if (conditionExpression.ConditionValue == null)
                    {
                        sb.Append(" is null");
                    }
                    else
                    {
                        sb.Append("=");
                        hasParameter = true;
                    }
                    break;
                case ConditionOperator.GreaterThan:
                    sb.Append(">");
                    hasParameter = true;
                    break;
                case ConditionOperator.GreaterThanOrEqual:
                    sb.Append(">=");
                    hasParameter = true;
                    break;
                case ConditionOperator.LessThan:
                    sb.Append("<");
                    hasParameter = true;
                    break;
                case ConditionOperator.LessThanOrEqual:
                    sb.Append("<=");
                    hasParameter = true;
                    break;
                case ConditionOperator.NotEqual:
                    if (conditionExpression.ConditionValue == null)
                    {
                        sb.Append(" is not null");
                    }
                    else
                    {
                        sb.Append(" <>");
                        hasParameter = true;
                    }
                    break;
                case ConditionOperator.In:
                case ConditionOperator.NotIn:
                    if (conditionExpression.ConditionOperator == ConditionOperator.NotIn)
                    {
                        sb.Append(" not");
                    }
                    sb.Append(" in ");
                    if (objValue is IQueryExpression)
                    {
                        hasParameter = true;
                    }
                    else
                    {
                        string v = conditionExpression.ConditionValue + "";
                        v = v.Trim();
                        if (!v.StartsWith("("))
                        {
                            sb.Append("(");
                        }
                        sb.Append(v);
                        if (!v.EndsWith(")"))
                        {
                            sb.Append(")");
                        }
                    }
                    break;
                case ConditionOperator.Like:
                case ConditionOperator.LeftLike:
                case ConditionOperator.RightLike:
                    sb.Append(" like ");
                    hasParameter = true;
                    break;
                case ConditionOperator.NotLike:
                case ConditionOperator.NotLeftLike:
                case ConditionOperator.NotRightLike:
                    sb.Append(" not like ");
                    hasParameter = true;
                    break;
                case ConditionOperator.Custom:
                    sb.Append(conditionExpression.ConditionValue);
                    break;
            }
            if (hasParameter)
            {
                if (objValue is IQueryExpression)
                {
                    IQueryExpression query = objValue as IQueryExpression;
                    SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse( mappingProvider, query);
                    sb.Append(" (");
                    GenerateSelectSql_SelectFromWhereOrder(sb, parameters, ref parameterCounter, query, sqlAnalyseResult, mappingProvider,null);
                    sb.Append(")");
                }
                else
                {
                    sb.Append(CreateParameterNameInStatement(parameterCounter));
                    Type propertyType = sqlColumn.FullPropertyName.GroupFunction == GroupFunction.Count ? typeof(int) : sqlColumn.Column.PropertyType;
                    int size = sqlColumn.FullPropertyName.GroupFunction == GroupFunction.Count ? 4 : sqlColumn.Column.Size;
                    AddParameter(parameters, ref parameterCounter, objValue, propertyType, size);
                }
            }
        }



        #endregion

        protected virtual IDbCommand CreateCommand(IDbHelper session, IMappingProvider mappingProvider,object commandType, IDictionary<string, object> values)
        { 

            IStatement statement = mappingProvider.GetStatement(commandType);

             
            IDbCommand dbCommand = session.Connection.CreateCommand();
            dbCommand.CommandText = statement.CommandText;
            dbCommand.CommandType = statement.CommandType;

            foreach (KeyValuePair<string, IParameter> kv in statement.Parameters)
            {
                object peropertyValue = values[kv.Key];
                IParameter parameter = kv.Value;
                IDataParameter dataParameter = CreateParameter(parameter.ParameterName, peropertyValue, parameter.PropertyType, parameter.Size);
                dataParameter.Direction = parameter.Direction;
                dbCommand.Parameters.Add(dataParameter);
            }

            return dbCommand;
        }

        protected virtual IDataParameter AddParameter(IDataParameterCollection parameters, ref int parameterCounter, object parameterValue, Type propertyType, int size)
        {
            IDataParameter parameter = CreateParameter(CreateParameterName(parameterCounter++) , parameterValue, propertyType, size);
            parameters.Add(parameter);
            return parameter;
        }
        protected string GetTableName(object entityType, IMappingProvider mappingProvider, IDictionary<object, string> tableNameMapping)
        {
            if (tableNameMapping != null && tableNameMapping.ContainsKey(entityType))
            {
                //如果tableMapping中包含键则返回，否则去缓存中取
                return tableNameMapping[entityType];
            }
            ITable table = mappingProvider.GetTable(entityType);
            return table.TableName;
        }
        #endregion

        #region 子类必须实现的方法
        protected abstract string CreateParameterName(int parameterCounter);
        protected abstract string CreateParameterNameInStatement(int parameterCounter);
        protected abstract IDataParameter CreateParameter(string parameterName, object parameterValue, Type propertyType, int size); 
        #endregion
 
    }
}
