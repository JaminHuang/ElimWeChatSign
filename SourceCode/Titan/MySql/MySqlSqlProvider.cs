using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;
using System.Data.Common;
using Titan.ExpressionAnalyse;
using System.ComponentModel;

namespace Titan.MySql
{
    public class MySqlSqlProvider : SqlProvider
    {



        public override IDbConnection CreateConnection()
        {
            return new MySqlConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }


        public override void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex)
        {
            MySqlCommand mysqlCommand = (MySqlCommand)command;
            Util.CheckPageSizeAndPageIndex(pageSize, pageIndex);


            StringBuilder sb = new StringBuilder();
            sb.Append(command.CommandText);
            sb.Append(" limit ?PagerPageSize");
            sb.Append(" offset ?PagerOffset");



            int pagerOffset = (pageIndex - 1) * pageSize;
            MySqlParameter pPageSize = new MySqlParameter("?PagerPageSize", DbType.Int32);
            pPageSize.Value = pageSize;
            mysqlCommand.Parameters.Add(pPageSize);
            MySqlParameter pPageOffset = new MySqlParameter("?PagerOffset", DbType.Int32);
            pPageOffset.Value = pagerOffset;
            mysqlCommand.Parameters.Add(pPageOffset);

            mysqlCommand.CommandText = sb.ToString();
        }

        public override object ConvertDbValue(object value, Type expectedType)
        {
            if (value == null || value == DBNull.Value) return null;
            if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type innerType = Nullable.GetUnderlyingType(expectedType);
                return ConvertDbValue(value, innerType);
            }
            if (expectedType.IsEnum)
            {
                Type innerType = Enum.GetUnderlyingType(expectedType);
                return Enum.ToObject(expectedType, ConvertDbValue(value, innerType));
            }

            return Convert.ChangeType(value, expectedType);
        }


        protected override string CreateParameterName(int parameterCounter)
        {
            return "?" + parameterCounter;
        }
        protected override string CreateParameterNameInStatement(int parameterCounter)
        {
            return "?" + parameterCounter;
        }
        protected override IDataParameter CreateParameter(string parameterName, object parameterValue, Type propertyType, int length)
        {
            var parameter = new MySqlParameter { ParameterName = parameterName };
            parameter.MySqlDbType = GetMySqlDbType(propertyType);
            if (length > 0 && (parameter.MySqlDbType == MySqlDbType.VarChar || parameter.MySqlDbType == MySqlDbType.VarString))
            {
                parameter.Size = length;
            }
            if (parameterValue == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                if (parameterValue is bool)
                {
                    parameter.Value = (bool)parameterValue ? 1 : 0;
                }
                else if (parameterValue is Enum)
                {
                    parameter.Value = (int)parameterValue;
                }
                else
                {
                    parameter.Value = parameterValue;
                }
            }
            return parameter;
        }


        private static readonly Dictionary<Type, MySqlDbType> CachedSqlServerDbTypes = new Dictionary<Type, MySqlDbType> { { typeof(byte[]), MySqlDbType.Blob } };
        /// <summary>
        /// 根据类型获取存储过程参数类型
        /// </summary>
        /// <param name="parameterValueType"></param>
        /// <returns></returns>
        private static MySqlDbType GetMySqlDbType(Type parameterValueType)
        {
            if (!CachedSqlServerDbTypes.ContainsKey(parameterValueType))
            {
                lock (CachedSqlServerDbTypes)
                {
                    if (!CachedSqlServerDbTypes.ContainsKey(parameterValueType))
                    {
                        Type innerType = parameterValueType;
                        if (innerType.IsGenericType && innerType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            innerType = Nullable.GetUnderlyingType(innerType);
                        }
                        //nullable中可能还有枚举
                        if (innerType.IsEnum)
                        {
                            innerType = Enum.GetUnderlyingType(innerType);
                        }

// ReSharper disable once RedundantAssignment
                        MySqlDbType sqlDbType = MySqlDbType.VarString;
                        MySqlParameter param = new MySqlParameter();
                        TypeConverter tc = TypeDescriptor.GetConverter(param.DbType);
                        if (tc.CanConvertFrom(innerType))
                        {
                            param.DbType = (DbType)tc.ConvertFrom(innerType.Name);
                        }
                        else
                        {
                            try
                            {
                                param.DbType = (DbType)tc.ConvertFrom(innerType.Name);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        sqlDbType = param.MySqlDbType;

                        CachedSqlServerDbTypes.Add(parameterValueType, sqlDbType);
                    }
                }
            }
            return CachedSqlServerDbTypes[parameterValueType];
        }

        #region 对象操作

        public override int Insert(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, IDictionary<string, object> inserts, IDictionary<string, object> returns)
        {
            ITable table = mappingProvider.GetTable(entityType);


            var sb = new StringBuilder();
            var command = new MySqlCommand();
            int parameterCounter = 1;

            #region 循环每个属性
            string tableName = GetTableName(entityType, mappingProvider, tableMapping);
            sb.Append("insert into ");
            sb.Append(tableName);
            sb.Append(" (");
            //Oracle存储过程参数有顺序之分
            //List<OracleParameter> returnParameters = new List<OracleParameter>();//取回的存储过程
            Dictionary<string, string> returnColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); //属性名是键，sql参数名是值

            int index = 0;
            foreach (KeyValuePair<string, object> kv in inserts)
            {
                if (index > 0) { sb.Append(","); }

                var column = table.Columns[kv.Key];
                if (column.InsertBehavior.Generate)
                {
                    sb.Append(column.ColumnName);
                    index++;
                }
            }
            sb.Append(") values (");


            index = 0;
            foreach (KeyValuePair<string, object> kv in inserts)
            {
                if (index > 0) { sb.Append(","); }
                IColumn column = table.Columns[kv.Key];
                if (column.InsertBehavior.Generate)
                {
                    if (column.InsertBehavior.ValueBehavior == ValueBehavior.UseValueExpression)
                    {
                        sb.Append(column.InsertBehavior.ValueExpression);
                    }
                    else
                    {
                        sb.Append(CreateParameterNameInStatement(parameterCounter));


                        //object propertyValue = column.PropertyAdapter.GetValue(obj);
                        //Validate(column, propertyValue);//验证如：检查字符串长度，是否为null
                        AddParameter(command.Parameters, ref parameterCounter, kv.Value, column.PropertyType, column.Size);

                    }
                    index++;
                }
            }
            sb.Append(")");


            foreach (string propertyName in table.PrimaryProperties)
            {
                ColumnWithIdentity column = (ColumnWithIdentity)table.Columns[propertyName];
                //取回主键值 ，本版本只支持主键值的取回
                if (column.IsIdentity && column.ReturnAfterInsert)
                {
                    string pname = CreateParameterName(parameterCounter);
                    returnColumns.Add(propertyName, pname);

                    sb.Append(";select LAST_INSERT_ID();");
                    break;//只能有一个
                }
            }
            #endregion


            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;


            //执行sql
            object returnValue = db.ExecuteScalar(command);

            //read return value  
            foreach (KeyValuePair<string, string> item in returnColumns)
            {

                returns.Add(item.Key, ConvertDbValue(returnValue, table.Columns[item.Key].PropertyType));
                break;//只能有一个
            } 

            return 1;
        }

        public override SelectionResult SelectCollection(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, IQueryExpression queryExpression)
        {
            //检查pageSize和pageIndex是否合法
            Util.CheckPageSizeAndPageIndex(queryExpression.PageSize, queryExpression.PageIndex);

            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            var sb = new StringBuilder();
            var cmd = db.Connection.CreateCommand();
            int counter = 1;


            GenerateSelectSql_SelectFromWhereOrder(sb, cmd.Parameters, ref counter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);


            //执行sql
            cmd.CommandText = sb.ToString();
            cmd.CommandType = CommandType.Text;
            if (queryExpression.PageIndex != null && queryExpression.PageSize != null)
            {
                //分页的时候需要包装分页语句
                WrapPaging(db, cmd, queryExpression.PageSize.Value, queryExpression.PageIndex.Value);
            }


            SelectionResult result = new SelectionResult();
            result.ObjectFiller = sqlAnalyseResult.ObjectFiller;
            if (queryExpression.ReturnMatchedCount)
            {
                if (sqlAnalyseResult.HasGroup)
                {
                    result.TotalMatchedCount = SelectCountGroup(db, mappingProvider, tableMapping, queryExpression.EntityType, queryExpression, sqlAnalyseResult);
                }
                else
                {
                    result.TotalMatchedCount = SelectCount(db, mappingProvider, tableMapping, queryExpression.EntityType, queryExpression.Wheres);
                }
            }


            result.DataReader = db.ExecuteReader(cmd);
            return result;
        }
        public int SelectCountGroup(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, IQueryExpression queryExpression, SqlAnalyseResult sqlAnalyseResult)
        {
            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            //创建一个QueryExpression 


            sb.Append("select count(0) from (");
            //select
            sb.Append("select 1 as A_A_A ");


            //from where
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);

            sb.Append(") a");








            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            IDataReader dataReader = db.ExecuteReader(command);


            int count = 0;
            if (dataReader.Read())
            {
                count = Convert.ToInt32(dataReader[0]);
            }
            dataReader.Close();
            return count;
        }
        public override int SelectCount(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, ICondition condition)
        {


            var sb = new StringBuilder();
            var command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            //创建一个QueryExpression
            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition);
            var sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);



            sb.Append("select count(0) ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);







            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            IDataReader dataReader = db.ExecuteReader(command);


            int count = 0;
            if (dataReader.Read())
            {
                count = Convert.ToInt32(dataReader[0]);
            }
            dataReader.Close();
            return count;
        }

        public override bool Exists(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, ICondition condition)
        {
            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition);
            var sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            var sb = new StringBuilder();
            var command = db.Connection.CreateCommand();
            int parameterCounter = 1;



            sb.Append("select 0 ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);
            sb.Append(" limit 1");

            //sb.Append(conditionCollection.Count > 0 ? " and " : " where ");
            //sb.Append(" rownum<2");




            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            IDataReader dataReader = db.ExecuteReader(command);


            bool exists = dataReader.Read();
            dataReader.Close();
            return exists;
        }

        public override int Delete(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, ICondition condition)
        {
            //ITable table=getTables().getTableByClass(clazz);

            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition);
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);



            //执行关联删除 
            var sb = new StringBuilder();
            var command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            sb.Append("delete ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);

            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            int returnCount = db.ExecuteNonQuery(command);


            return returnCount;
        }

        public override int Update(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, ICondition condition, IDictionary<string, object> updates)
        {
            ITable table = mappingProvider.GetTable(entityType);

            QueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            foreach (string propertyName in updates.Keys)
            {
                queryExpression.Selects.Add(propertyName);
            }
            queryExpression.Wheres.Add(condition);

            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);




            var sb = new StringBuilder();
            var command = db.Connection.CreateCommand();
            int parameterCounter = 1;


            int index;

            sb.Append("update ");
            sb.Append(GetTableName(entityType, mappingProvider, tableMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);



            //join
            foreach (SqlTable sqlTable in sqlAnalyseResult.ForeignTables)
            {
                sb.Append(" left outer join ");
                sb.Append(GetTableName(entityType, mappingProvider, tableMapping));
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

            sb.Append(" set ");
            index = 0;
            foreach (KeyValuePair<string, object> kv in updates)
            {
                if (index > 0) { sb.Append(","); }
                index++;
                IColumn column = table.Columns[kv.Key];
                sb.Append(sqlAnalyseResult.MasterTableNameAlias);
                sb.Append(".");
                sb.Append(column.ColumnName);
                sb.Append("=");
                sb.Append(CreateParameterNameInStatement(parameterCounter));


                //object propertyValue = column.PropertyAdapter.GetValue(obj);
                //Validate(column, propertyValue);//验证如：检查字符串长度，是否为null
                AddParameter(command.Parameters, ref parameterCounter, kv.Value, column.PropertyType, column.Size);

            }

            //where 
            if (queryExpression.Wheres.Count > 0)
            {
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult, true, mappingProvider);
            }



            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            int returnCount = db.ExecuteNonQuery(command);


            return returnCount;
        }

        #endregion





        protected override void GenerateSelectSql_Where_Item(StringBuilder sb, IDataParameterCollection parameters, ref int parameterCounter, IConditionExpression conditionExpression, SqlColumn sqlColumn, bool includeTableNameAlias, IMappingProvider mappingProvider)
        {
            if (conditionExpression.ConditionOperator == ConditionOperator.FullTextLike || conditionExpression.ConditionOperator == ConditionOperator.NotFullTextLike)
            {
                //sql server 全文索引总是contains
                //if (string.IsNullOrWhiteSpace(sqlColumn.Column.FullTextSearch))
                //{
                //    throw ExceptionFactory.FullTextSearchNotSupported(sqlColumn.Column.PropertyAdapter.PropertyName);
                //}
                string fulltextSearch = sqlColumn.Column.FullTextSearch.ToLower();
                if (string.IsNullOrWhiteSpace(fulltextSearch)) fulltextSearch = "contains";
                string tag = conditionExpression.ConditionOperator == ConditionOperator.FullTextLike ? ">" : "<=";
                switch (fulltextSearch)
                {
                    case "contains":
                        sb.Append("contains(");
                        sb.Append(sqlColumn.TableAliasAndColumn);
                        sb.Append(",");
                        sb.Append(CreateParameterNameInStatement(parameterCounter));
                        sb.Append(",1)");
                        sb.Append(tag);
                        sb.Append("0");
                        break;
                    default:
                        //function({column},{parameter},1)>0
                        string s = fulltextSearch.Replace("{column}", sqlColumn.TableAliasAndColumn);
                        s = s.Replace("{parameter}", CreateParameterNameInStatement(parameterCounter));
                        sb.Append(s);
                        break;
                }
                AddParameter(parameters, ref parameterCounter, conditionExpression.ConditionValue, sqlColumn.Column.PropertyType, sqlColumn.Column.Size);
            }
            else
            {
                base.GenerateSelectSql_Where_Item(sb, parameters, ref parameterCounter, conditionExpression, sqlColumn, includeTableNameAlias, mappingProvider);
            }
        }


        public override IMappingProvider CreateDefaultMappingProvider()
        {
            return MySqlMappingProvider.Instance;
        }
    }
}
