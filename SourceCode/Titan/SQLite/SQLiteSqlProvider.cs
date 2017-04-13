using System;
using System.Collections.Generic; 
using System.Text;
using System.Data;
using Titan.ExpressionAnalyse;
using System.Data.Common;
using System.Data.SQLite;

namespace Titan.SQLite
{
    public class SQLiteSqlProvider : SqlProvider
    {
         


        public override IDbConnection CreateConnection()
        {
            return new SQLiteConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new SQLiteDataAdapter();
        }



        public override void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex)
        {
            SQLiteCommand sqliteCommand = (SQLiteCommand)command;
            Util.CheckPageSizeAndPageIndex(pageSize, pageIndex);


            StringBuilder sb = new StringBuilder();
            sb.Append(command.CommandText);
            sb.Append(" limit @PagerPageSize offset @PagerOffset");



            int pagerOffset = (pageIndex - 1) * pageSize;
            sqliteCommand.Parameters.Add("@PagerPageSize", DbType.Int32).Value = pageSize;
            sqliteCommand.Parameters.Add("@PagerOffset", DbType.Int32).Value = pagerOffset;

            sqliteCommand.CommandText = sb.ToString();
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
        protected override IDataParameter CreateParameter(string parameterName, object parameterValue, Type propertyType,int length)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = parameterName;

            if (parameterValue == null)
            {
                parameter.DbType = GetDbType(propertyType);
                //parameter.Value = Util.GetDefaultValue(propertyType);
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
                    parameter.Value = Convert.ChangeType(parameterValue,Enum.GetUnderlyingType(propertyType));
                }
                else
                {
                    parameter.Value = parameterValue;
                }
            }
            return parameter;
        }
        private static readonly Dictionary<Type, DbType> CachedOracleDbTypes = new Dictionary<Type, DbType>() { { typeof(bool), DbType.Byte } };
        /// <summary>
        /// 根据类型获取存储过程参数类型
        /// </summary>
        /// <param name="parameterValueType"></param>
        /// <returns></returns>
        private static DbType GetDbType(Type parameterValueType)
        {
            if (!CachedOracleDbTypes.ContainsKey(parameterValueType))
            {
                lock (CachedOracleDbTypes)
                {
                    if (!CachedOracleDbTypes.ContainsKey(parameterValueType))
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

                        SQLiteParameter param = new SQLiteParameter { Value = Util.GetDefaultValue(innerType) };
                        CachedOracleDbTypes.Add(parameterValueType, param.DbType);
                    }
                }
            }
            return CachedOracleDbTypes[parameterValueType];
        }
         

        #region 对象操作

        public override int Insert(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, object entityType, IDictionary<string, object> inserts, IDictionary<string, object> returns)
        { 
            ITable table = mappingProvider.GetTable(entityType); 


            StringBuilder sb = new StringBuilder();
            SQLiteCommand command = new SQLiteCommand();
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
            //var propertyNames = insertPropertyNames as string[] ?? insertPropertyNames.ToArray();
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
                        sb.Append(CreateParameterNameInStatement (parameterCounter));


                        //object propertyValue = column.PropertyAdapter.GetValue(obj);
                        //Validate(column, propertyValue);//验证如：检查字符串长度，是否为null
                        AddParameter(command.Parameters, ref parameterCounter, kv.Value, column.PropertyType, column.Size);

                    }
                    index++;
                }
            }
            sb.Append(")");

            //"INSERT INTO SequenceTest_Table (ID, OtherColumn)" +
            //"VALUES (SequenceTest_Sequence.NEXTVAL, :OtherColumn)" +
            //"RETURNING ID INTO :ID";
            foreach ( string propertyName in table.PrimaryProperties)
            { 
                ColumnWithIdentity column=(ColumnWithIdentity)table.Columns[propertyName];
                //取回主键值 ，本版本只支持主键值的取回
                if (column.IsIdentity && column.ReturnAfterInsert)
                {
                    string pname=CreateParameterName( parameterCounter);
                    returnColumns.Add(propertyName, pname); 

                    sb.Append(";select last_insert_rowid()");
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
            
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider,queryExpression);


            StringBuilder sb = new StringBuilder();
            IDbCommand cmd = db.Connection.CreateCommand();
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
                    result.TotalMatchedCount = SelectCountGroup(db,mappingProvider, tableMapping, queryExpression.EntityType, queryExpression, sqlAnalyseResult);
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
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter,  queryExpression, sqlAnalyseResult,mappingProvider, tableMapping);

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
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            //创建一个QueryExpression
            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition);
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);



            sb.Append("select count(0) ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult,mappingProvider, tableMapping);







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
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;



            sb.Append("select 0 ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping); 
            sb.Append(" limit 1");




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

            if (sqlAnalyseResult.ForeignTables.Count > 0)
            {
                //不支持
                throw ExceptionFactory.NotSupported("SQLite不支持多表关联更新");
            }

            //执行关联删除 
            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            string tableName = GetTableName(entityType, mappingProvider, tableMapping);
            sb.Append("delete from ");
            sb.Append(tableName); 
            //说明是无关联表的条件，只是本表的条件
            sb.Append(" where ");
            GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter,   queryExpression.Wheres,sqlAnalyseResult,false,mappingProvider);


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


            if (sqlAnalyseResult.ForeignTables.Count > 0)
            {
                //不支持
                throw ExceptionFactory.NotSupported("SQLite不支持多表关联更新");
            }

            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;


            string tableName = GetTableName(entityType, mappingProvider, tableMapping);
            sb.Append("update ");
            sb.Append(tableName);
            sb.Append(" set ");
            int index = 0;
            foreach (KeyValuePair<string, object> kv in updates)
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
            //说明是无关联表的条件，只是本表的条件
            sb.Append(" where ");
            GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, condition, sqlAnalyseResult, false, mappingProvider);


            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            int returnCount = db.ExecuteNonQuery(command);


            return returnCount;
        }
        #endregion

        protected override string CreateParameterName(int parameterCounter)
        {
            return "@" + parameterCounter;
        }
        protected override string CreateParameterNameInStatement(int parameterCounter)
        {
            return "@" + parameterCounter;
        }
        public override IMappingProvider CreateDefaultMappingProvider()
        { 
            return SQLiteMappingProvider.Instance;
        }
    }
}
