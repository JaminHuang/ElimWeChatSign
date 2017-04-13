using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Data.Common;
using Titan.ExpressionAnalyse;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Titan.SqlServer
{
    public class SqlServerSqlProvider : SqlProvider
    { 

 

        public override IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }


        public override void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex)
        {
            var cmd = (SqlCommand)command;
            Util.CheckPageSizeAndPageIndex(pageSize, pageIndex);

            int pageUpperBound = pageIndex * pageSize ;
            int pageLowerBound = (pageIndex - 1) * pageSize;

            SqlConnection cn = db.Connection as SqlConnection;
            string ver = cn.ServerVersion;
            string[] ss = ver.Split('.');
            int version = 0;
            try { version = Convert.ToInt32(ss[0]); }
            catch { }


            var sb = new StringBuilder();

            if (version <= 8)
            {
                //sql server 2000或以前的版本
                sb.Append("SELECT ");
                sb.Append(" a.*, Identity(int,1,1) AS __RN__ INTO #TMP FROM ( ");


                string parttern2000 = @"^select\s+top\s+";
                if (Regex.IsMatch(command.CommandText, parttern2000, RegexOptions.IgnoreCase))
                {
                    sb.Append(" SELECT TOP ");
                    sb.Append(pageUpperBound);
                    sb.Append(" * from (");
                    sb.Append(command.CommandText);
                    sb.Append(") aa ");
                }
                else
                {
                    string sql2 = Regex.Replace(command.CommandText, @"^select\s+", "SELECT TOP " + pageUpperBound + " ", RegexOptions.IgnoreCase);
                    sb.Append(sql2);
                }


                sb.Append(") a;SELECT * FROM #TMP WHERE __RN__ > ");
                sb.Append(pageLowerBound);
                sb.Append(";DROP TABLE #TMP;");
            }
            else
            {
                sb.Append("with t as ( ");
                sb.Append("select  a.*,ROW_NUMBER() over( order by (select 0)) as __RN__ from (");//__rn__必须是最后一列，否则读取数据时顺序错乱

                string parttern2005 = @"^select\s+top\s+";
                if (Regex.IsMatch(command.CommandText, parttern2005, RegexOptions.IgnoreCase))
                {
                    //说明已经包含 top
                    sb.Append(" SELECT TOP ");
                    sb.Append(pageUpperBound);
                    sb.Append(" * from (");
                    sb.Append(command.CommandText);
                    sb.Append(") aa ");
                }
                else
                {
                    string sql2 = Regex.Replace(command.CommandText, @"^select\s+", "SELECT TOP " + pageUpperBound + " ", RegexOptions.IgnoreCase);
                    sb.Append(sql2);
                }

                sb.Append(") a");
                sb.Append(")  select * from t where __RN__> @PageLowerBound");
                sb.Append(" and __RN__ <=@PageUpperBound");


                cmd.Parameters.Add("@PageUpperBound", SqlDbType.Int).Value = pageUpperBound;
                cmd.Parameters.Add("@PageLowerBound", SqlDbType.Int).Value = pageLowerBound;
            }


 

            cmd.CommandText = sb.ToString();
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
            return "@" + parameterCounter;
        }
        protected override string CreateParameterNameInStatement(int parameterCounter)
        {
            return "@" + parameterCounter;
        }
        protected override IDataParameter CreateParameter(string parameterName, object parameterValue, Type propertyType, int size)
        {
            var parameter = new SqlParameter { ParameterName = parameterName };
            parameter.SqlDbType = GetSqlDbType(propertyType);
            if (size > 0 && (parameter.SqlDbType == SqlDbType.Char || parameter.SqlDbType == SqlDbType.NChar || parameter.SqlDbType == SqlDbType.VarChar || parameter.SqlDbType == SqlDbType.NVarChar))
            {
                parameter.Size = size;
            }


            if (parameterValue == null)
            {
                parameter.Value = DBNull.Value;
            }
            else if (parameterValue is bool)
            {
                parameter.Value = (bool)parameterValue ? 1 : 0;
            }
            else if (parameterValue is Enum)
            {
                parameter.Value = (int)parameterValue;
            }
            else if (parameterValue is string)
            {
                //henry注入
                Guid guid = Guid.Empty;
                string strGuid = parameterValue.ToString();
                if ((strGuid.Length == 36 || strGuid.Length == 32) && Guid.TryParse(strGuid, out guid) && propertyType==typeof(Guid))
                {
                    parameter.Value = guid;
                }
                else
                {
                    parameter.Value = parameterValue;
                }
            }
            else
            {
                parameter.Value = parameterValue;
            }


            return parameter;
        }


        private static readonly Dictionary<Type, SqlDbType> CachedSqlServerDbTypes = new Dictionary<Type, SqlDbType>() { { typeof(byte[]), SqlDbType.Image } };
        /// <summary>
        /// 根据类型获取存储过程参数类型
        /// </summary>
        /// <param name="parameterValueType"></param>
        /// <returns></returns>
        private static SqlDbType GetSqlDbType(Type parameterValueType)
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

                        SqlDbType sqlDbType = SqlDbType.NVarChar;
                        SqlParameter param = new SqlParameter();
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
                        sqlDbType = param.SqlDbType;

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


            StringBuilder sb = new StringBuilder();
            SqlCommand command = new SqlCommand();
            int parameterCounter = 0;

            StringBuilder sbValues = new StringBuilder();
            Dictionary<string, string> returnColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); //属性名是键，sql参数名是值
            string tableName = GetTableName(entityType,mappingProvider, tableMapping);

            sb.Append("insert into ");
            sb.Append(tableName);
            sb.Append(" (");

            int index = 0; 
            foreach (KeyValuePair<string,object> kv in inserts)
            {
                var column = table.Columns[kv.Key];
                if (column.InsertBehavior.Generate)
                {
                    if (index > 0) { sb.Append(","); sbValues.Append(","); }
                    index++;

                    sb.Append(column.ColumnName);

                    //sbValues 
                    if (column.InsertBehavior.ValueBehavior == ValueBehavior.UseValueExpression)
                    {
                        sbValues.Append(column.InsertBehavior.ValueExpression);
                    }
                    else
                    {
                        sbValues.Append(CreateParameterName(parameterCounter));


                        //object propertyValue = column.PropertyAdapter.GetValue(entity);
                        //Validate(column, propertyValue);//验证如：检查字符串长度，是否为null
                        AddParameter(command.Parameters, ref parameterCounter, kv.Value, column.PropertyType, column.Size);

                    }
                }
            }
            sb.Append(") values (");
            sb.Append(sbValues);
            sb.Append(")"); 

            foreach ( string propertyName in table.PrimaryProperties)
            {
                SqlServerColumn column = (SqlServerColumn)table.Columns[propertyName];
                //取回主键值 ，本版本只支持主键值的取回
                if (column.IsIdentity && column.ReturnAfterInsert)
                {
                    string pname=CreateParameterName( parameterCounter);
                    returnColumns.Add(propertyName, pname);
                     

                    sb.Append(";set ");
                    sb.Append(CreateParameterName (parameterCounter));
                    sb.Append("=SCOPE_Identity()");

                    var parameter = (SqlParameter)AddParameter(command.Parameters, ref parameterCounter,  null, column.PropertyType, column.Size);
                    parameter.Direction = ParameterDirection.Output;

                }
            }  


            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;


            //执行sql
            int returnCount = db.ExecuteNonQuery(command);

            //read return value  
            foreach (KeyValuePair<string, string> item in returnColumns)
            {
                object objValue = command.Parameters[item.Value].Value;  //GetCommandValue(cmd, pName);
                returns.Add(item.Key, ConvertDbValue(objValue, table.Columns[item.Key].PropertyType)); 
                //returns.Add(item.Key, objValue);
                //item.Value.PropertyAdapter.SetValue(entity, ConvertDbValue(objValue, item.Value.PropertyAdapter.PropertyType.Type));
            }

            return returnCount;
        }

        public override SelectionResult SelectCollection(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping,  IQueryExpression queryExpression )
        {
            //检查pageSize和pageIndex是否合法
            Util.CheckPageSizeAndPageIndex(queryExpression.PageSize, queryExpression.PageIndex);


            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            var sb = new StringBuilder();
            var cmd = db.Connection.CreateCommand();
            int counter = 1;


            GenerateSelectSql_SelectFromWhereOrder(sb, cmd.Parameters, ref counter, queryExpression, sqlAnalyseResult,mappingProvider, tableMapping);


            //执行sql
            cmd.CommandText = sb.ToString();
            cmd.CommandType = CommandType.Text;
            if (queryExpression.PageIndex != null && queryExpression.PageSize != null)
            {
                //分页的时候需要包装分页语句
                WrapPaging(db,cmd, queryExpression.PageSize.Value, queryExpression.PageIndex.Value);
            }

            SelectionResult result = new SelectionResult();
            result.ObjectFiller = sqlAnalyseResult.ObjectFiller;

            //先关闭datarader
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


            StringBuilder sb = new StringBuilder();
            SqlCommand command = (SqlCommand)db.Connection.CreateCommand();
            int parameterCounter = 1;

            //创建一个QueryExpression
            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition) ;
            var sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);



            sb.Append("select count(0) ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter,   queryExpression, sqlAnalyseResult,mappingProvider, tableMapping);







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


            StringBuilder sb = new StringBuilder();
            SqlCommand command = (SqlCommand)db.Connection.CreateCommand();
            int parameterCounter = 1;



            sb.Append("select top 1 1 ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter,queryExpression, sqlAnalyseResult,mappingProvider, tableMapping);

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
            sb.Append(" from ");
            sb.Append(GetTableName(entityType,mappingProvider, tableMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
            if (sqlAnalyseResult.ForeignTables.Count == 0)
            {
                //说明是无关联表的条件，只是本表的条件
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult,true,mappingProvider);
            }
            else
            {
                //说明有关联表的条件，需要多表关联
                sb.Append(" where exists (");
                SqlServerCreateSqlExists(sb, command.Parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult,mappingProvider, tableMapping);
                sb.Append(")");
            }

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



            sb.Append("update ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
            sb.Append(" set ");
            var index = 0;
            foreach (KeyValuePair<string,object> kv in updates)
            {
                if (index > 0) { sb.Append(","); }
                index++;
                IColumn column = table.Columns[kv.Key];
                sb.Append(column.ColumnName);
                sb.Append("=");
                sb.Append(CreateParameterNameInStatement(parameterCounter));


                //object propertyValue = column.PropertyAdapter.GetValue(obj);
                //Validate(column, propertyValue);//验证如：检查字符串长度，是否为null
                AddParameter(command.Parameters, ref parameterCounter, kv.Value, column.PropertyType, column.Size);

            }
            sb.Append(" from ");
            sb.Append(GetTableName(entityType,mappingProvider, tableMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
            sb.Append(" ");
            if (sqlAnalyseResult.ForeignTables.Count == 0)
            {
                //说明是无关联表的条件，只是本表的条件
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, condition, sqlAnalyseResult,true,mappingProvider);
            }
            else
            {
                //说明有关联表的条件，需要多表关联
                sb.Append(" where exists (");
                SqlServerCreateSqlExists(sb, command.Parameters, ref parameterCounter, condition, sqlAnalyseResult,mappingProvider,tableMapping);
                sb.Append(")");
            }

            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            int returnCount = db.ExecuteNonQuery(command);


            return returnCount;
        }
        
        #endregion       
        
         

        private void SqlServerCreateSqlExists(
            StringBuilder sb, IDataParameterCollection parameters, ref int parameterCounter,
            ICondition condition, SqlAnalyseResult sqlAnalyseResult, IMappingProvider mappingProvider,
            IDictionary<object, string> tableMapping
            )
        {


            int tableIndex = 0;
            if (sqlAnalyseResult.ForeignTables.Count > 0)
            {
                sb.Append("select 1 from ");
                foreach (var sqlTable in sqlAnalyseResult.ForeignTables)
                {
                    if (tableIndex > 0)
                    {
                        sb.Append(",");
                    }
                    tableIndex++;
                    sb.Append(GetTableName(sqlTable.ForeignEntityType, mappingProvider, tableMapping));
                    sb.Append(" ");
                    sb.Append(sqlTable.ForeignTableNameAlias);
                }
                tableIndex = 0;
                foreach (var sqlTable in sqlAnalyseResult.ForeignTables)
                {
                    foreach (var sqlRelationColumn in sqlTable.RelationColumns)
                    {
                        sb.Append(tableIndex > 0 ? " and " : " where ");
                        tableIndex++;

                        sb.Append(sqlRelationColumn.Expression);
                    }
                }
            }

            //condition
            //if (condition.Count <= 0) return;
            sb.Append(tableIndex > 0 ? " and " : " where ");
            GenerateSelectSql_Where(sb, parameters, ref parameterCounter,
                                    condition, sqlAnalyseResult, true, mappingProvider);
            //order 不需要order
        }


        protected override void GenerateSelectSql_Where_Item(StringBuilder sb, IDataParameterCollection parameters, ref int parameterCounter, IConditionExpression conditionExpression, SqlColumn sqlColumn, bool includeTableNameAlias,IMappingProvider mappingProvider)
        {
            if (conditionExpression.ConditionOperator == ConditionOperator.FullTextLike || conditionExpression.ConditionOperator == ConditionOperator.NotFullTextLike)
            {
                //sql server 全文索引总是contains
                //if (string.IsNullOrWhiteSpace(sqlColumn.Column.FullTextSearch))
                //{
                //    throw ExceptionFactory.FullTextSearchNotSupported(sqlColumn.Column.PropertyAdapter.PropertyName);
                //}
                string fulltextSearch = (sqlColumn.Column.FullTextSearch + "").ToLower();
                if (string.IsNullOrWhiteSpace(fulltextSearch)) fulltextSearch = "contains";
                string tag = conditionExpression.ConditionOperator == ConditionOperator.FullTextLike ? "" : " not ";
                switch (fulltextSearch)
                {
                    case "contains":
                        sb.Append(tag);
                        sb.Append("contains(");
                        sb.Append(sqlColumn.TableAliasAndColumn);
                        sb.Append(",");
                        sb.Append(CreateParameterNameInStatement( parameterCounter));
                        sb.Append(")");
                        break;
                    default:
                        //function({column},{parameter},1)>0
                        string s = fulltextSearch.Replace("{column}", sqlColumn.TableAliasAndColumn);
                        s = s.Replace("{parameter}", CreateParameterNameInStatement(   parameterCounter));
                        sb.Append(s);
                        break;
                }
                AddParameter(parameters,ref parameterCounter, conditionExpression.ConditionValue, sqlColumn.Column.PropertyType, sqlColumn.Column.Size);
            }
            else if (conditionExpression.ConditionOperator == ConditionOperator.Equal)
            {
                SqlServerColumn sqlserverColumn = (SqlServerColumn)sqlColumn.Column;
                if (sqlserverColumn.IsNText)
                {
                    sb.Append(sqlColumn.TableAliasAndColumn);
                    if (conditionExpression.ConditionValue == null)
                    {
                        sb.Append(" is null");
                    }
                    else
                    {
                        sb.Append(" like ");
                        sb.Append(CreateParameterNameInStatement(parameterCounter));
                        AddParameter(parameters, ref parameterCounter, conditionExpression.ConditionValue, sqlColumn.Column.PropertyType, sqlColumn.Column.Size);
                    }
                }
                else
                {
                    base.GenerateSelectSql_Where_Item(sb, parameters, ref parameterCounter, conditionExpression, sqlColumn, includeTableNameAlias,mappingProvider);
                }
            }
            else
            {
                base.GenerateSelectSql_Where_Item(sb, parameters, ref parameterCounter, conditionExpression, sqlColumn, includeTableNameAlias, mappingProvider);
            }
        }

        public override IMappingProvider CreateDefaultMappingProvider()
        {
            return SqlServerMappingProvider.Instance;
        }
    }
}
