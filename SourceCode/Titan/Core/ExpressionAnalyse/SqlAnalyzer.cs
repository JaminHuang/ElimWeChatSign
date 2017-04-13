using System;
using System.Collections.Generic; 
using System.Text;

namespace Titan.ExpressionAnalyse
{
    internal static class SqlAnalyzer
    {
         

        //缓存
        private static readonly object SqlAnalyseResultsLock = new object();
        internal static readonly Dictionary<string, SqlAnalyseResult> SqlAnalyseResults = new Dictionary<string, SqlAnalyseResult>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 收集queryExpression中所有用的到属性名
        /// </summary>
        /// <param name="mappingProvider"></param>
        /// <param name="queryExpression"></param>
        /// <returns></returns>
        public static SqlAnalyseResult Analyse(IMappingProvider mappingProvider, IQueryExpression queryExpression)
        {
            //使用sorted是为了判断gethashcode时是否缓存中已经存在
            //在分析时taskPropertyNames只是判断是否要输出，是否有合计函数
            SortedDictionary<IPropertyExpression, string> taskPropertyNames = new SortedDictionary<IPropertyExpression, string>();

            //必须先分析输出部分，因为输出部分包含是否要填充的信息
            CollectPropertyNames(taskPropertyNames, queryExpression.Selects);
            CollectPropertyNames(taskPropertyNames, queryExpression.Wheres);
            CollectPropertyNames(taskPropertyNames, queryExpression.OrderBys);


            CollectPropertyNames(taskPropertyNames, queryExpression.Havings);
            CollectPropertyNames(taskPropertyNames, queryExpression.GroupBys);

            return Analyse(mappingProvider, queryExpression.EntityType, taskPropertyNames);
        }

        public static SqlAnalyseResult Analyse(IMappingProvider mappingProvider, object entityType, SortedDictionary<IPropertyExpression, string> taskPropertyNames)
        {
            if (!Config.UseExpressionAnalyseCache)
            {
                SqlAnalyseResult sqlAnalyseResult = new SqlAnalyseResult();
                int tableIndexCounter = 0;
                int outputColumnIndexCounter = 0;//注意在.net中输出的列是从0开始的
                sqlAnalyseResult.ObjectFiller.EntityType = entityType;
                AnalyseInner(mappingProvider, entityType, sqlAnalyseResult, sqlAnalyseResult.ObjectFiller, taskPropertyNames, null, ref tableIndexCounter, ref outputColumnIndexCounter);
                return sqlAnalyseResult;

            }

            //注意不同实体类型可能有相同的字段因此必须传入entityType
            string hashKey = GetHashKey(entityType, taskPropertyNames);
            if (!SqlAnalyseResults.ContainsKey(hashKey))
            {
                lock (SqlAnalyseResultsLock)
                {
                    if (!SqlAnalyseResults.ContainsKey(hashKey))
                    {
                        SqlAnalyseResult sqlAnalyseResult = new SqlAnalyseResult();

                        int tableIndexCounter = 0;
                        int outputColumnIndexCounter = 0;//注意在.net中输出的列是从0开始的
                        sqlAnalyseResult.ObjectFiller.EntityType = entityType;
                        AnalyseInner(mappingProvider, entityType, sqlAnalyseResult, sqlAnalyseResult.ObjectFiller, taskPropertyNames, null, ref tableIndexCounter, ref outputColumnIndexCounter);

                        //添加到缓存
                        SqlAnalyseResults.Add(hashKey, sqlAnalyseResult);
                    }
                }
            }
            return SqlAnalyseResults[hashKey];
        }




        private static void AnalyseInner( IMappingProvider mappingProvider, object entityType,
                SqlAnalyseResult sqlAnalyseResult, ObjectFiller objectFiller,
                IDictionary<IPropertyExpression, string> taskPropertyNames, string prefix,
                ref int tableIndexCounter, ref int outputColumnIndexCounter
                )
        {


            ITable table = mappingProvider.GetTable(entityType);


            string currentTableNameAlias = "t" + tableIndexCounter;
            if (prefix == null)
            {
                //前缀为null时表示是起始对象
                sqlAnalyseResult.MasterEntityType = entityType;
                sqlAnalyseResult.MasterTableNameAlias = currentTableNameAlias;
            }


            Dictionary<string, Dictionary<IPropertyExpression, string>> taskObjects = new Dictionary<string, Dictionary<IPropertyExpression, string>>(StringComparer.OrdinalIgnoreCase);
            objectFiller.PropertyFillers = new List<PropertyFiller>();
            foreach (IPropertyExpression propertyName in taskPropertyNames.Keys)
            {
                PrefixParser prefixParser = new PrefixParser(propertyName.PropertyName);
                if (prefixParser.HasPrefix)
                {
                    //说明是外键对象的属性，例如"Person.City.CityId"
                    if (!taskObjects.ContainsKey(prefixParser.Prefix))
                    {
                        taskObjects.Add(prefixParser.Prefix, new Dictionary<IPropertyExpression, string>());
                    }
                    taskObjects[prefixParser.Prefix].Add(new PropertyExpression(prefixParser.PropertyName, propertyName.GroupFunction), taskPropertyNames[propertyName]);
                }
                else
                {
                    //说明是本对象的属性
                    string joinedPropertyName = PrefixParser.JoinPrefix(prefix, propertyName.PropertyName);


                    IColumn column = null;
                    if (joinedPropertyName == "***")
                    {
                        //伪造一个column
                        //column = sqlProvider.CreateCountColumn();
                    }
                    else
                    {
                        //如果不为***则column不会为null
                        if (!table.Columns.ContainsKey(propertyName.PropertyName))
                        {
                            throw new MissingMemberException(entityType.ToString(), propertyName.PropertyName);
                        }
                        column = table.Columns[propertyName.PropertyName];
                    }



                    if (!sqlAnalyseResult.HasGroup && propertyName.GroupFunction != GroupFunction.None)
                    {
                        sqlAnalyseResult.HasGroup = true;
                    }

                    //sqlColumn
                    SqlColumn sqlColumn = new SqlColumn();
                    sqlColumn.FullPropertyName = new PropertyExpression(joinedPropertyName, propertyName.GroupFunction);
                    sqlColumn.Column = column;


                    //oracle等数据库需要别名 
                    StringBuilder sb = new StringBuilder();
                    if (propertyName.GroupFunction != GroupFunction.None)
                    {
                        sb.Append(propertyName.GroupFunction.ToString());
                        sb.Append("(");
                    }
                    if (propertyName.GroupFunction == GroupFunction.Count)
                    {
                        sb.Append("*");
                    }
                    else
                    {
                        sb.Append(currentTableNameAlias);
                        sb.Append(".");
                        sb.Append(column.ColumnName);
                    }
                    if (propertyName.GroupFunction != GroupFunction.None)
                    {
                        sb.Append(")");
                    }
                    sqlColumn.TableAliasAndColumn = sb.ToString();


                    //filler
                    if (taskPropertyNames[propertyName]!=null)
                    {
                        //只有输出字段才有PropertyFiller
                        PropertyFiller propertyFiller = new PropertyFiller();
                        if (propertyName.GroupFunction == GroupFunction.None)
                        {
                            propertyFiller.PropertyName = column.PropertyName;
                            propertyFiller.PropertyType = column.PropertyType;
                        }
                        else
                        {
                            if (!table.GroupColumns.ContainsKey(propertyName))
                            {
                                throw new MissingMemberException(string.Format("类型{0}缺少标记为GroupFunction.{1}的GroupColumnAttribute", entityType, propertyName.GroupFunction));
                            }
                            IGroupColumn groupColumn = table.GroupColumns[propertyName];
                            propertyFiller.PropertyName = groupColumn.PropertyName;
                            propertyFiller.PropertyType = groupColumn.PropertyType;
                        }
                        propertyFiller.ColumnIndex = outputColumnIndexCounter;

                        string outputSqlColumn = taskPropertyNames[propertyName];
                        if (outputSqlColumn == "")
                        {
                            //如果为空格，则使用默认f开头的计数器作为别名
                            sb.Append(" f");
                            sb.Append(outputColumnIndexCounter);
                            outputColumnIndexCounter++;
                            outputSqlColumn = sb.ToString();
                        } 

                        sqlAnalyseResult.SortedOutputColumns.Add(outputSqlColumn);

                        objectFiller.PropertyFillers.Add(propertyFiller);
                    }


                    sqlAnalyseResult.SqlColumns.Add(sqlColumn.FullPropertyName, sqlColumn);
                }
            }


            //分析父对象，必须在自己属性分析完后才进行 
            objectFiller.ObjectFillers = new List<ObjectFiller>();
            foreach (string objectPeropertyName in taskObjects.Keys)
            {
                string tableNameAlias = "t" + ++tableIndexCounter;


                if (!table.Relations.ContainsKey(objectPeropertyName))
                {
                    throw new MissingMemberException(entityType.ToString(), objectPeropertyName);
                }
                IRelation relation= table.Relations[objectPeropertyName];

                SqlTable sqlTable = new SqlTable();
                sqlTable.TableNameAlias = currentTableNameAlias;
                sqlTable.ForeignTableNameAlias = tableNameAlias;
                sqlTable.ForeignEntityType = relation.ForeignEntityType;


                List<SqlRelationColumn> sqlRelationColumns = new List<SqlRelationColumn>();
                foreach (IRelationColumn relationColumn in relation.RelationColumns)
                {
                    SqlRelationColumn sqlRelationColumn = new SqlRelationColumn();
                    sqlRelationColumn.ColumnName = relationColumn.ColumnName;
                    sqlRelationColumn.ForeignColumnName = relationColumn.ForeignColumnName;


                    StringBuilder sb = new StringBuilder();
                    sb.Append(sqlTable.TableNameAlias);
                    sb.Append('.');
                    sb.Append(relationColumn.ColumnName);
                    sb.Append('=');
                    sb.Append(sqlTable.ForeignTableNameAlias);
                    sb.Append('.');
                    sb.Append(relationColumn.ForeignColumnName);
                    sqlRelationColumn.Expression = sb.ToString();

                    sqlRelationColumns.Add(sqlRelationColumn);
                }
                sqlTable.RelationColumns = sqlRelationColumns;

                sqlAnalyseResult.ForeignTables.Add(sqlTable);




                ObjectFiller subObjectFiller = new ObjectFiller();
                subObjectFiller.PropertyName = relation.PropertyName;
                subObjectFiller.EntityType = relation.ForeignEntityType;
                objectFiller.ObjectFillers.Add(subObjectFiller);

                //AnalyseInner(sqlProvider, relation.PropertyAdapter.PropertyType, sqlAnalyseResult, subObjectFiller,taskObjects[objectPeropertyName], objectPeropertyName, tableIndexCounter, outputColumnIndexCounter);

                string joinedPropertyName = PrefixParser.JoinPrefix(prefix, relation.PropertyName);
                AnalyseInner(mappingProvider, relation.ForeignEntityType, sqlAnalyseResult, subObjectFiller, taskObjects[relation.PropertyName], joinedPropertyName, ref tableIndexCounter, ref outputColumnIndexCounter);

            }

        }


        #region CollectPropertyNames


        /// <summary>
        /// 值用来存储别名，注意：空格表示输出但是无alias
        /// </summary>
        /// <param name="taskPropertyNames"></param>
        /// <param name="selects"></param>
        private static void CollectPropertyNames(SortedDictionary<IPropertyExpression, string> taskPropertyNames, IEnumerable<IOutputExpression> selects)
        {
            if (selects != null)
            {
                foreach (IOutputExpression obj in selects)
                {
                    string propertyName = obj.Property.GroupFunction == GroupFunction.Count ? "***" : obj.Property.PropertyName;
                    PropertyExpression p = new PropertyExpression(propertyName, obj.Property.GroupFunction);
                    if (!taskPropertyNames.ContainsKey(p))
                    {
                        taskPropertyNames.Add(p, obj.Alias+"");//需要输出，所以值为true
                    }
                }
            }
        }


        private static void CollectPropertyNames(SortedDictionary<IPropertyExpression, string> taskPropertyNames, IEnumerable<IGroupExpression> groups)
        {
            
            if (groups != null)
            {
                foreach (IGroupExpression obj in groups)
                {
                    PropertyExpression p = new PropertyExpression(obj.PropertyName,GroupFunction.None);
                    if (!taskPropertyNames.ContainsKey(p))
                    {
                        taskPropertyNames.Add(p, null);//不需要输出，由于总是OutputExpression.Selects先分析，所以这里可以加入null
                    } 
                }
            }
        }

        private static void CollectPropertyNames(SortedDictionary<IPropertyExpression, string> taskPropertyNames, IEnumerable<IOrderExpression> orders)
        {
            if (orders != null)
            {
                foreach (IOrderExpression obj in orders)
                {
                    if (!taskPropertyNames.ContainsKey(obj.Property))
                    {
                        taskPropertyNames.Add(obj.Property, null);//不需要输出，所以值为false
                    }
                }
            }
        }
        private static void CollectPropertyNames(SortedDictionary<IPropertyExpression, string> taskPropertyNames, ICondition wheres)
        {
            if (wheres != null)
            {
                if (wheres is IConditionExpressionCollection)
                {
                    IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)wheres;
                    foreach (ICondition obj in conditionCollection)
                    {
                        CollectPropertyNames(taskPropertyNames, obj);
                    }
                }
                else
                {
                    IConditionExpression conditionItem = (IConditionExpression)wheres;
                    if (!taskPropertyNames.ContainsKey(conditionItem.Property))
                    {
                        taskPropertyNames.Add(conditionItem.Property, null);
                    }
                }
            }
        }


        #endregion




        private static string GetHashKey(object entityType, SortedDictionary<IPropertyExpression, string> allPropertyNames)
        {
            //使用sorted是为了判断gethashcode时是否缓存中已经存在，
            //避免字段都相同而顺序不同造成的错误判断
            StringBuilder sb = new StringBuilder();
            sb.Append(entityType); //注意不同实体类型可能有相同的字段因此必须传入entityType
            sb.Append(':');
            foreach (IPropertyExpression s in allPropertyNames.Keys)
            {
                sb.Append(s);
                if (allPropertyNames[s] != null)
                {
                    sb.Append(",output");
                }
                sb.Append(';');
            }
            return sb.ToString();
        }



    }
}
