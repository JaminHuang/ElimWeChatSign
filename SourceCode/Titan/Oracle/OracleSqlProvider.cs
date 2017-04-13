using System;
using System.Collections.Generic; 
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Titan.ExpressionAnalyse;
using System.Data.Common;
using Oracle.ManagedDataAccess.Types;

namespace Titan.Oracle
{
    public class OracleSqlProvider : SqlProvider
    {
        //private const string PARAMETER_PREFIX="v" ;
        //private const string PARAMETER_PREFIX_IN_STATEMENT = ":v";
        //private const string PARAMETER_TAG = ":";  


        public override IDbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new OracleDataAdapter();
        }




        public override void WrapPaging(IDbHelper db, IDbCommand command, int pageSize, int pageIndex)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            Util.CheckPageSizeAndPageIndex(pageSize, pageIndex);



            StringBuilder sb = new StringBuilder();
            sb.Append("select * from ( select ta.*, rownum rn from ( ");
            sb.Append(command.CommandText);
            sb.Append(") ta where rownum < :vPageUpperBound");
            sb.Append(") tb where rn > :vPageLowerBound");



            int pageUpperBound = pageIndex * pageSize + 1;
            int pageLowerBound = (pageIndex - 1) * pageSize;
            oracleCommand.Parameters.Add("vPageUpperBound", OracleDbType.Int32).Value = pageUpperBound;
            oracleCommand.Parameters.Add("vPageLowerBound", OracleDbType.Int32).Value = pageLowerBound;

            oracleCommand.CommandText = sb.ToString();
        }
        protected override string CreateParameterName(int parameterCounter)
        {
            return "v" + parameterCounter;
        }
        protected override string CreateParameterNameInStatement(int parameterCounter)
        {
            return ":v" + parameterCounter;
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
            if (value is OracleDecimal)
            {
                OracleDecimal newValue = (OracleDecimal)value;
                if (expectedType == typeof(byte))
                {
                    return newValue.ToByte();
                }
                if (expectedType == typeof(Int16))
                {
                    return newValue.ToInt16();
                }
                if (expectedType == typeof(Int32))
                {
                    return newValue.ToInt32();
                }
                if (expectedType == typeof(Int64))
                {
                    return newValue.ToInt64();
                }
                if (expectedType == typeof(Single))
                {
                    return newValue.ToSingle();
                }
                if (expectedType == typeof(Double))
                {
                    return newValue.ToDouble();
                }
                if (expectedType == typeof(bool))
                {
                    return Convert.ToBoolean(newValue.ToInt32());
                }
                throw new Exception("未实现Oracle.ManagedDataAccess.Types.OracleDecimal转化" + expectedType);
            }
            return Convert.ChangeType(value, expectedType);
        }
        protected override IDataParameter CreateParameter(string parameterName, object parameterValue, Type propertyType, int length)
        {
            global::Oracle.ManagedDataAccess.Client.OracleParameter parameter = new global::Oracle.ManagedDataAccess.Client.OracleParameter();
            parameter.ParameterName = parameterName;

            if (parameterValue == null)
            {
                parameter.OracleDbType = GetOracleDbType(propertyType);
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
                    parameter.Value = Convert.ChangeType(parameterValue, Enum.GetUnderlyingType(propertyType));
                }
                else
                {
                    parameter.Value = parameterValue;
                }
            }
            if (length > 0 && (parameter.OracleDbType == OracleDbType.Char || parameter.OracleDbType == OracleDbType.NChar || parameter.OracleDbType == OracleDbType.Varchar2 || parameter.OracleDbType == OracleDbType.NVarchar2))
            {
                parameter.Size = length;
            }
            return parameter;
        }
        private static readonly Dictionary<Type, OracleDbType> CachedOracleDbTypes = new Dictionary<Type, OracleDbType>() { 
            { typeof(bool), OracleDbType.Byte }, 
            { typeof(bool?), OracleDbType.Byte },
            { typeof(short), OracleDbType.Int16 }, 
            { typeof(short?), OracleDbType.Int16 }, 
            { typeof(int), OracleDbType.Int32 }, 
            { typeof(int?), OracleDbType.Int32 }, 
            { typeof(long), OracleDbType.Int64 }, 
            { typeof(long?), OracleDbType.Int64 },  
            { typeof(float), OracleDbType.Single }, 
            { typeof(float?), OracleDbType.Single }, 
            { typeof(double), OracleDbType.Double }, 
            { typeof(double?), OracleDbType.Double }, 
            { typeof(decimal), OracleDbType.Decimal }, 
            { typeof(decimal?), OracleDbType.Decimal },  
            { typeof(DateTime), OracleDbType.Date }, 
            { typeof(DateTime?), OracleDbType.Date }, 
            { typeof(String), OracleDbType.Varchar2 },  
            { typeof(byte[]), OracleDbType.Blob } 
        };
        /// <summary>
        /// 根据类型获取存储过程参数类型
        /// </summary>
        /// <param name="parameterValueType"></param>
        /// <returns></returns>
        public static OracleDbType GetOracleDbType(Type parameterValueType)
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

                        global::Oracle.ManagedDataAccess.Client.OracleParameter param = new global::Oracle.ManagedDataAccess.Client.OracleParameter { Value = Util.GetDefaultValue(innerType) };
                        CachedOracleDbTypes.Add(parameterValueType, param.OracleDbType);
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
            OracleCommand command = new OracleCommand();
            int parameterCounter = 1;




            #region 循环每个属性
            string tableName = GetTableName(entityType, mappingProvider, tableMapping);
            sb.Append("begin insert into ");
            sb.Append(tableName);
            sb.Append(" (");
            //Oracle存储过程参数有顺序之分
            //List<OracleParameter> returnParameters = new List<OracleParameter>();//取回的存储过程
            Dictionary<string, string> returnColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); //属性名是键，sql参数名是值

            int index = 0;
            foreach (KeyValuePair<string, object> kv in inserts)
            {
                if (index > 0) { sb.Append(","); }

                IColumn column = table.Columns[kv.Key];
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

            //"INSERT INTO SequenceTest_Table (ID, OtherColumn)" +
            //"VALUES (SequenceTest_Sequence.NEXTVAL, :OtherColumn)" +
            //"RETURNING ID INTO :ID";

            foreach (string propertyName in table.PrimaryProperties)
            {
                OracleColumn column = (OracleColumn)table.Columns[propertyName];
                //取回主键值 ，本版本只支持主键值的取回
                if (column.HasSequence && column.ReturnAfterInsert)
                {
                    string pname = CreateParameterName(parameterCounter);
                    returnColumns.Add(propertyName, pname);

                    sb.Append(";select ");
                    sb.Append(column.Sequence);
                    sb.Append(".currval into ");
                    sb.Append(CreateParameterNameInStatement(parameterCounter));
                    sb.Append(" from dual");


                    global::Oracle.ManagedDataAccess.Client.OracleParameter parameter = (global::Oracle.ManagedDataAccess.Client.OracleParameter)AddParameter(command.Parameters, ref parameterCounter, null, column.PropertyType, column.Size);
                    parameter.Direction = ParameterDirection.Output;

                }
            }
            sb.Append("; end;");
            #endregion


            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;


            //执行sql
            int returnCount = db.ExecuteNonQuery(command);

            //read return value  
            foreach (KeyValuePair<string, string> item in returnColumns)
            {
                object objValue = command.Parameters[item.Value].Value;  //GetCommandValue(cmd, pName);
                returns.Add(item.Key, ConvertDbValue(objValue, table.Columns[item.Key].PropertyType));
                //item.Value.PropertyAdapter.SetValue(obj, ConvertDbValue(objValue, item.Value.PropertyAdapter.PropertyType.Type));
            }

            return returnCount;
        }

        public override SelectionResult SelectCollection(IDbHelper db, IMappingProvider mappingProvider, IDictionary<object, string> tableMapping, IQueryExpression queryExpression)
        {
            //检查pageSize和pageIndex是否合法
            Util.CheckPageSizeAndPageIndex(queryExpression.PageSize, queryExpression.PageIndex);


            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            StringBuilder sb = new StringBuilder();
            IDbCommand cmd = db.Connection.CreateCommand();
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
            sb.Append("select 1 as A_A_A");


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
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;

            //创建一个QueryExpression
            IQueryExpression queryExpression = new QueryExpression();
            queryExpression.EntityType = entityType;
            queryExpression.Wheres.Add(condition);
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);



            sb.Append("select count(*) ");
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
            SqlAnalyseResult sqlAnalyseResult = SqlAnalyzer.Analyse(mappingProvider, queryExpression);


            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;



            sb.Append("select 0 ");
            GenerateSelectSql_FromWhere(sb, command.Parameters, ref parameterCounter, queryExpression, sqlAnalyseResult, mappingProvider, tableMapping);

            if (Util.HasCondition(condition))
            {
                sb.Append(" and ");
            }
            else
            {
                sb.Append(" where ");
            }
            sb.Append(" rownum<2");




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
            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;


            sb.Append("delete from ");
            sb.Append(GetTableName(entityType, mappingProvider, tableMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
            if (sqlAnalyseResult.ForeignTables.Count == 0)
            {
                //说明是无关联表的条件，只是本表的条件
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult, true, mappingProvider);
            }
            else
            {
                //说明有关联表的条件，需要多表关联
                sb.Append(" where exists (");
                Oracle_CreateSqlExists(sb, command.Parameters, ref parameterCounter, queryExpression.Wheres, sqlAnalyseResult, mappingProvider, tableMapping);
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


            StringBuilder sb = new StringBuilder();
            IDbCommand command = db.Connection.CreateCommand();
            int parameterCounter = 1;



            sb.Append("update ");
            sb.Append(GetTableName(entityType, mappingProvider, tableMapping));
            sb.Append(" ");
            sb.Append(sqlAnalyseResult.MasterTableNameAlias);
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
            if (sqlAnalyseResult.ForeignTables.Count == 0)
            {
                //说明是无关联表的条件，只是本表的条件
                sb.Append(" where ");
                GenerateSelectSql_Where(sb, command.Parameters, ref parameterCounter, condition, sqlAnalyseResult, true, mappingProvider);
            }
            else
            {
                //说明有关联表的条件，需要多表关联
                sb.Append(" where exists (");
                Oracle_CreateSqlExists(sb, command.Parameters, ref parameterCounter, condition, sqlAnalyseResult, mappingProvider, tableMapping);
                sb.Append(")");
            }

            //执行sql
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            int returnCount = db.ExecuteNonQuery(command);


            return returnCount;
        }
        #endregion

        private void Oracle_CreateSqlExists(
            StringBuilder sb, IDataParameterCollection parameters, ref int parameterCounter,
            ICondition condition, SqlAnalyseResult sqlAnalyseResult, IMappingProvider mappingProvider,
            IDictionary<object, string> tableMapping
            )
        {


            sb.Append("select 1 from ");

            int tableIndex = 0;
            if (sqlAnalyseResult.ForeignTables.Count > 0)
            {
                foreach (SqlTable sqlTable in sqlAnalyseResult.ForeignTables)
                {
                    if (tableIndex > 0) { sb.Append(","); }
                    tableIndex++;
                    sb.Append(GetTableName(sqlTable.ForeignEntityType, mappingProvider, tableMapping));
                    sb.Append(" ");
                    sb.Append(sqlTable.ForeignTableNameAlias);
                }
                tableIndex = 0;
                foreach (SqlTable sqlTable in sqlAnalyseResult.ForeignTables)
                {
                    foreach (SqlRelationColumn sqlRelationColumn in sqlTable.RelationColumns)
                    {
                        sb.Append(tableIndex > 0 ? " and " : " where ");
                        tableIndex++;

                        sb.Append(sqlRelationColumn.Expression);
                    }
                }
            }
            else
            {
                sb.Append("dual");
            }
            //condition

            sb.Append(tableIndex > 0 ? " and " : " where ");
            //tableIndex++; 这句不能有
            GenerateSelectSql_Where(sb, parameters, ref parameterCounter, condition, sqlAnalyseResult, true, mappingProvider);



            //order 不需要order

        }


        protected override IDbCommand CreateCommand(IDbHelper db, IMappingProvider mappingProvider, object commandType, IDictionary<string, object> values)
        {
            IStatement statement = mappingProvider.GetStatement(commandType);


            StringBuilder sb = new StringBuilder();
            IDbCommand dbCommand = db.Connection.CreateCommand();
            dbCommand.CommandText = statement.CommandText;
            dbCommand.CommandType = statement.CommandType;

            foreach (KeyValuePair<string, IParameter> kv in statement.Parameters)
            {
                object peropertyValue = values[kv.Key];
                OracleParameter parameter = (OracleParameter)kv.Value;
                global::Oracle.ManagedDataAccess.Client.OracleParameter dataParameter = null;
                if (parameter.IsCursor)
                {
                    dataParameter = new global::Oracle.ManagedDataAccess.Client.OracleParameter();
                    dataParameter.ParameterName = parameter.ParameterName;
                    dataParameter.OracleDbType = OracleDbType.RefCursor;
                    CreateParameter(parameter.ParameterName, peropertyValue, parameter.PropertyType, parameter.Size);
                }
                else
                {
                    dataParameter = (global::Oracle.ManagedDataAccess.Client.OracleParameter)CreateParameter(parameter.ParameterName, peropertyValue, parameter.PropertyType, parameter.Size);
                }
                dataParameter.Direction = parameter.Direction;
                dbCommand.Parameters.Add(dataParameter);
            }

            return dbCommand;
        }

        protected override void GenerateSelectSql_Where_Item(StringBuilder sb, IDataParameterCollection parameters, ref int parameterCounter, IConditionExpression conditionExpression, SqlColumn sqlColumn, bool includeTableNameAlias, IMappingProvider mappingProvider)
        {
            if (conditionExpression.ConditionOperator == ConditionOperator.FullTextLike || conditionExpression.ConditionOperator == ConditionOperator.NotFullTextLike)
            {
                if (string.IsNullOrWhiteSpace(sqlColumn.Column.FullTextSearch))
                {
                    throw ExceptionFactory.FullTextSearchNotSupported(sqlColumn.Column.PropertyName);
                }
                string fulltextSearch = sqlColumn.Column.FullTextSearch;
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
                    case "catsearch":
                        sb.Append("catsearch(");
                        sb.Append(sqlColumn.TableAliasAndColumn);
                        sb.Append(",");
                        sb.Append(CreateParameterNameInStatement(parameterCounter));
                        sb.Append(",null)");
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
            return OracleMappingProvider.Instance;
        }
    }
}
