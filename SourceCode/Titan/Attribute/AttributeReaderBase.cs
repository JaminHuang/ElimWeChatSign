using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace Titan
{
    public class AttributeReaderBase<T, C, S, P, TA, CA, SA, PA> : IAttributeReader
        where T : ITable
        where C : IColumn
        where S : IStatement
        where P : IParameter
        where TA : TableAttributeBase
        where CA : ColumnAttributeBase
        where SA : StatementAttributeBase
        where PA : ParameterAttributeBase
    {

        public virtual ITable ReadTableAttribute(Type entityType)
        {
            T table = Activator.CreateInstance<T>();

            TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(entityType, typeof(TableAttribute), true);
            TA attr = (TA)Attribute.GetCustomAttribute(entityType, typeof(TA), true);
            if (tableAttribute == null)
            {
                if (attr != null)
                {
                    throw AttributeException.TableAttributeNotFoundException(entityType, typeof(TA));
                }
                else
                {
                    //throw ExceptionFactory.AttributeNotFoundException(entityType.Type, typeof(TableAttribute));
                    return null;
                }
            }

            SetTableValue(table, entityType, tableAttribute, attr);
            return table;
        }
        protected void SetTableValue(T table, Type entityType, TableAttribute tableAttribute, TableAttributeBase tableAttributeBase)
        {
            table.TableName = string.IsNullOrWhiteSpace(tableAttribute.TableName) ? entityType.Name : tableAttribute.TableName;





            table.Columns = new Dictionary<string, IColumn>(StringComparer.OrdinalIgnoreCase);//不区分大小写
            table.Relations = new Dictionary<string, IRelation>(StringComparer.OrdinalIgnoreCase);//不区分大小写
            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IColumn column = ReadColumnAttribute(propertyInfo);
                if (column == null)
                {
                    IRelation relation = ReadRelationAttribute(propertyInfo);
                    if (relation == null)
                    {
                        IGroupColumn groupColumn = ReadGroupColumnAttribute(propertyInfo);
                        if (groupColumn != null)
                        {
                            //table.AddGroupColumn(groupColumn);
                            //table.GroupColumns.Add(new PropertyExpression(propertyInfo.Name, groupColumn.GroupFunction), groupColumn);
                            table.GroupColumns.Add(new PropertyExpression(groupColumn.OriginalPropertyName, groupColumn.GroupFunction), groupColumn);
                        }
                    }
                    else 
                    {
                        table.Relations.Add(propertyInfo.Name, relation);
                    }
                }
                else
                {
                    table.Columns.Add(propertyInfo.Name, column);
                }
            }
            FieldInfo[] fieldInfos = entityType.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                IColumn column = ReadColumnAttribute(fieldInfo);
                if (column == null)
                {
                    IRelation relation = ReadRelationAttribute(fieldInfo);
                    if (relation == null)
                    {
                        IGroupColumn groupColumn = ReadGroupColumnAttribute(fieldInfo);
                        if (groupColumn != null)
                        {
                            //table.AddGroupColumn(groupColumn);
                            table.GroupColumns.Add(new PropertyExpression(fieldInfo.Name, groupColumn.GroupFunction), groupColumn);
                        }
                    }
                    else
                    {
                        table.Relations.Add(fieldInfo.Name, relation);
                    }
                }
                else
                {
                    table.Columns.Add(fieldInfo.Name, column);
                }
            }




            #region 覆盖
            if (tableAttributeBase != null)
            {
                if (!string.IsNullOrWhiteSpace(tableAttributeBase.TableName))
                {
                    table.TableName = tableAttributeBase.TableName;//覆盖
                }
            }
            #endregion
        }



        public virtual IRelation ReadRelationAttribute(MemberInfo memberInfo)
        {
            RelationBase relation = new RelationBase();

            RelationAttribute relationAttribute = (RelationAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(RelationAttribute), true);
            if (relationAttribute == null) return null;
             
            if (memberInfo is PropertyInfo)
            {
                PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                relation.ForeignEntityType = propertyInfo.PropertyType;
            }
            else
            {
                FieldInfo fieldInfo = (FieldInfo)memberInfo;
                relation.ForeignEntityType = fieldInfo.FieldType;
            }
            relation.PropertyName = memberInfo.Name;
            List<IRelationColumn> relationColumns = new List<IRelationColumn>();
            foreach (RelationAttribute.RelationProperty relationProperty in relationAttribute.RelationPropertys)
            {
                IRelationColumn relationColumn = new RelationColumnBase();
                relationColumn.PropertyName = relationProperty.PropertyName;
                relationColumn.ForeignPropertyName = relationProperty.ForeignPropertyName;
                relationColumns.Add(relationColumn);
            }
            relation.RelationColumns = relationColumns.ToArray();
            return relation;
        }
         


        public virtual IColumn ReadColumnAttribute(MemberInfo memberInfo)
        {
            C column = Activator.CreateInstance<C>();
            ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute), true);
            CA sqlColumnAttribute = (CA)Attribute.GetCustomAttribute( memberInfo, typeof(CA), true);

            if (columnAttribute == null)
            {
                if (sqlColumnAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(CA));
                }
                else
                {
                    return null;
                }
            }

            
            SetColumnValue(column, memberInfo, columnAttribute, sqlColumnAttribute);

             
            return column;
        }
        
        protected void SetColumnValue(C column, MemberInfo memberInfo, ColumnAttribute columnAttribute, ColumnAttributeBase columnAttributeBase)
        {
            #region 不可覆盖的成员
            if (memberInfo is PropertyInfo)
            {
                PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                column.PropertyType = propertyInfo.PropertyType; 
            }
            else
            {
                FieldInfo fieldInfo = (FieldInfo)memberInfo;
                column.PropertyType = fieldInfo.FieldType;
            }
            column.PropertyName = memberInfo.Name;
            column.IsPrimaryKey = columnAttribute.IsPrimaryKey;
            column.ReturnAfterInsert = columnAttribute.ReturnAfterInsert == AttributeBoolean.Null ? false : columnAttribute.ReturnAfterInsert == AttributeBoolean.True;
            //column.DisplayName = string.IsNullOrWhiteSpace(columnAttribute.DisplayName) ? memberInfo.Name : columnAttribute.DisplayName;
            #endregion

            #region 可覆盖的成员
            column.ColumnName = string.IsNullOrWhiteSpace(columnAttribute.ColumnName) ? memberInfo.Name : columnAttribute.ColumnName;
            column.OutputAlias = column.ColumnName;
            column.FullTextSearch = string.IsNullOrWhiteSpace(columnAttribute.FullTextSearch) ? null : columnAttribute.FullTextSearch.ToLower();
            column.Size = columnAttribute.Size < 0 ? 0 : columnAttribute.Size;

            //创建Insert行为
            column.InsertBehavior = new ColumnSqlBehavior();
            column.InsertBehavior.Generate = columnAttribute.GenerateInsert == AttributeBoolean.Null ? true : columnAttribute.GenerateInsert == AttributeBoolean.True;
            if (!string.IsNullOrWhiteSpace(columnAttribute.DefaultValue))
            {
                column.InsertBehavior.ValueBehavior = ValueBehavior.UseValueExpression;
                column.InsertBehavior.ValueExpression = columnAttribute.DefaultValue;
            }
            else
            {
                column.InsertBehavior.ValueBehavior = ValueBehavior.UsePropertyValue;
                column.InsertBehavior.ValueExpression = null;
            }
            #endregion


            #region 覆盖
            if (columnAttributeBase != null)
            {
                if (!string.IsNullOrWhiteSpace(columnAttributeBase.ColumnName))
                {
                    column.ColumnName = columnAttributeBase.ColumnName;//覆盖
                }
                if (!string.IsNullOrWhiteSpace(columnAttributeBase.FullTextSearch))
                {
                    column.FullTextSearch = columnAttributeBase.FullTextSearch.ToLower();//覆盖
                }
                if (columnAttributeBase.GenerateInsert != AttributeBoolean.Null)
                {
                    column.InsertBehavior.Generate = columnAttributeBase.GenerateInsert == AttributeBoolean.True;//覆盖
                }
                if (columnAttributeBase.Size > -1) //默认值是-1，如果不是-1说明是标注过
                {
                    column.Size = columnAttributeBase.Size;//覆盖
                }
                if (!string.IsNullOrWhiteSpace(columnAttributeBase.DefaultValue))
                {
                    column.InsertBehavior.ValueExpression = columnAttributeBase.DefaultValue;//覆盖
                    column.InsertBehavior.ValueBehavior = ValueBehavior.UseValueExpression;
                }
            }
            #endregion
        }


        public virtual IGroupColumn ReadGroupColumnAttribute(MemberInfo memberInfo)
        {
            IGroupColumn groupColumn = new GroupColumnBase();
            GroupColumnAttribute groupColumnAttribute = (GroupColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(GroupColumnAttribute), true);

            if (groupColumnAttribute == null)
            {
                return null;
            }


            SetGroupColumnValue(groupColumn, memberInfo, groupColumnAttribute);


            return groupColumn;
        }

        protected void SetGroupColumnValue(IGroupColumn column, MemberInfo memberInfo, GroupColumnAttribute groupColumnAttribute)
        {
            #region 不可覆盖的成员
            if (memberInfo is FieldInfo)
            {
                column.PropertyType = ((FieldInfo)memberInfo).FieldType;
            }
            else
            {
                column.PropertyType = ((PropertyInfo)memberInfo).PropertyType;
            }
            column.GroupFunction = groupColumnAttribute.GroupFunction;

            column.OriginalPropertyName = column.GroupFunction == GroupFunction.Count ? "***" : groupColumnAttribute.OriginalPropertyName;
            column.PropertyName = memberInfo.Name;
            #endregion
             
        }

        public virtual IStatement ReadStatementAttribute(Type commandType)
        { 
            S statement = Activator.CreateInstance<S>();
            StatementAttribute statementAttribute = (StatementAttribute)Attribute.GetCustomAttribute(commandType, typeof(StatementAttribute), true);
            SA oracleStatementAttribute = (SA)Attribute.GetCustomAttribute(commandType, typeof(SA), true);
            if (statementAttribute == null)
            {
                if (oracleStatementAttribute != null)
                {
                    throw AttributeException.StatementAttributeNotFoundException(commandType, typeof(SA));
                }
                else
                {
                    return null;
                }
            }

            SetStatementValue(statement,commandType, statementAttribute, oracleStatementAttribute);
            return statement;
        }
        protected void SetStatementValue(S statement, Type entityType, StatementAttribute statementAttribute, StatementAttributeBase statementAttributeBase)
        {
            statement.CommandBehavior = statementAttribute.CommandBehavior;
            switch (statementAttribute.CommandType)
            {
                case AttributeCommandType.Null:
                case AttributeCommandType.StoredProcedure:
                    statement.CommandType = CommandType.StoredProcedure;
                    break;
                case AttributeCommandType.TableDirect:
                    statement.CommandType = CommandType.TableDirect;
                    break;
                case AttributeCommandType.Text:
                    statement.CommandType = CommandType.Text;
                    break;
            }
            statement.CommandText = string.IsNullOrWhiteSpace(statementAttribute.CommandText) ? entityType.Name : statementAttribute.CommandText;
            statement.Parameters = new Dictionary<string, IParameter>(StringComparer.OrdinalIgnoreCase);


            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IParameter parameter = ReadParameterAttribute(propertyInfo);
                if (parameter != null)
                {
                    statement.Parameters.Add(propertyInfo.Name, parameter);
                }

            }
            FieldInfo[] fieldInfos = entityType.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                IParameter parameter = ReadParameterAttribute(fieldInfo);
                if (parameter != null)
                {
                    statement.Parameters.Add(fieldInfo.Name, parameter);
                }
            }

            #region 覆盖
            if (statementAttributeBase != null)
            {
                if (statementAttributeBase.CommandType != AttributeCommandType.Null)
                {
                    switch (statementAttributeBase.CommandType)
                    {
                        case AttributeCommandType.StoredProcedure:
                            statement.CommandType = CommandType.StoredProcedure;
                            break;
                        case AttributeCommandType.TableDirect:
                            statement.CommandType = CommandType.TableDirect;
                            break;
                        case AttributeCommandType.Text:
                            statement.CommandType = CommandType.Text;
                            break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(statementAttributeBase.CommandText))
                {
                    statement.CommandText = statementAttributeBase.CommandText;//覆盖
                }
            }
            #endregion
        }


        public virtual IParameter ReadParameterAttribute(MemberInfo memberInfo)
        {
            P parameter = Activator.CreateInstance<P>();

            ParameterAttribute statementParameterAttribute = (ParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ParameterAttribute), true);
            PA sqlServerParameterAttribute = (PA)Attribute.GetCustomAttribute(memberInfo, typeof(PA), true);

            if (statementParameterAttribute == null)
            {
                if (sqlServerParameterAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(PA));
                }
                else
                {
                    return null;
                }
            }

            
            SetParameterValue(parameter, memberInfo, statementParameterAttribute, sqlServerParameterAttribute);

 

            return parameter;
        }
        


        protected void SetParameterValue(P parameter, MemberInfo memberInfo, ParameterAttribute statementParameterAttribute, ParameterAttributeBase statementParameterAttributeBase)
        {

            parameter.Direction = statementParameterAttribute.Direction;
            parameter.ParameterName = string.IsNullOrWhiteSpace(statementParameterAttribute.ParameterName) ? memberInfo.Name : statementParameterAttribute.ParameterName;
            parameter.Size = statementParameterAttribute.Size < 0 ? 0 : statementParameterAttribute.Size;
            //parameter.PropertyType = new PropertyAdapter(memberInfo);
            if (memberInfo is PropertyInfo)
            {
                PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                parameter.PropertyType = propertyInfo.PropertyType;
            }
            else
            {
                FieldInfo fieldInfo = (FieldInfo)memberInfo;
                parameter.PropertyType = fieldInfo.FieldType;
            }

            #region 覆盖
            if (statementParameterAttributeBase != null)
            {
                if (!string.IsNullOrWhiteSpace(statementParameterAttributeBase.ParameterName))
                {
                    parameter.ParameterName = statementParameterAttributeBase.ParameterName;//覆盖
                }
                if (statementParameterAttributeBase.Size > -1) //默认值是-1，如果不是-1说明是标注过
                {
                    parameter.Size = statementParameterAttributeBase.Size;//覆盖
                }
            }
            #endregion
        }

    
    }
}
