using System;
using System.Reflection;

namespace Titan.SqlServer
{
    internal class SqlServerAttributeReader : AttributeReaderBase<
        TableBase,
        SqlServerColumn,
        StatementBase,
        ParameterBase,
        SqlServerTableAttribute,
        SqlServerColumnAttribute,
        SqlServerStatementAttribute,
        SqlServerParameterAttribute>
    {

        public override ITable ReadTableAttribute(Type entityType)
        {
            TableBase table = new TableBase();
            TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(entityType, typeof(TableAttribute), true);
            SqlServerTableAttribute sqlServerTableAttribute = (SqlServerTableAttribute)Attribute.GetCustomAttribute(entityType, typeof(SqlServerTableAttribute), true);
            if (tableAttribute == null)
            {
                if (sqlServerTableAttribute != null)
                {
                    throw AttributeException.TableAttributeNotFoundException(entityType, typeof(SqlServerTableAttribute));
                }
                else
                {
                    throw AttributeException.AttributeNotFoundException(entityType, typeof(TableAttribute));
                }
            }

            SetTableValue(table, entityType, tableAttribute, sqlServerTableAttribute);

            if (string.IsNullOrWhiteSpace(tableAttribute.TableName)
               && (
                   sqlServerTableAttribute == null ||
                   (sqlServerTableAttribute != null && string.IsNullOrWhiteSpace(sqlServerTableAttribute.TableName))
                   )
               )
            {
                //针对sqlServer,如果某个属性的ColumnAttribute.ColumnName为空，则自动在字段名加[]
                table.TableName = "[" + entityType.Name + "]";
            }

            return table;
        }

        public override IColumn ReadColumnAttribute(MemberInfo memberInfo)
        {
            SqlServerColumn column = new SqlServerColumn();

            ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute), true);
            SqlServerColumnAttribute sqlColumnAttribute = (SqlServerColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(SqlServerColumnAttribute), true);

            if (columnAttribute == null)
            {
                if (sqlColumnAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(SqlServerColumnAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetColumnValue(column,memberInfo, columnAttribute, sqlColumnAttribute);

            if (string.IsNullOrWhiteSpace(columnAttribute.ColumnName)
                && (
                    sqlColumnAttribute == null ||
                    (sqlColumnAttribute != null && string.IsNullOrWhiteSpace(sqlColumnAttribute.ColumnName))
                    )
                )
            {
                //针对sqlServer,如果某个属性的ColumnAttribute.ColumnName为空，则自动在字段名加[]
                column.ColumnName = "[" + memberInfo.Name + "]";
            }
            //去除转义
            if (column.OutputAlias.StartsWith("[")) column.OutputAlias = column.OutputAlias.Substring(1, column.OutputAlias.Length - 1);
            if (column.OutputAlias.EndsWith("]")) column.OutputAlias = column.OutputAlias.Substring(0, column.OutputAlias.Length - 1);

            if (sqlColumnAttribute != null)
            {
                column.IsIdentity = sqlColumnAttribute.IsIdentity;
                if (column.IsIdentity)
                {
                    column.InsertBehavior.Generate = false;//如果是IsIdentity则强制不要在Insert语句中包含此列
                    if (columnAttribute.ReturnAfterInsert == AttributeBoolean.Null)
                    {
                        column.ReturnAfterInsert = true;//如果ReturnAfterInsert=null则默认会取回自动增长的值
                    }
                }
                column.IsNText = sqlColumnAttribute.IsNText;
            }
            return column;
        }

        public override IParameter ReadParameterAttribute(MemberInfo memberInfo)
        {
            ParameterBase parameter = new ParameterBase();

            ParameterAttribute statementParameterAttribute = (ParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ParameterAttribute), true);
            SqlServerParameterAttribute sqlServerParameterAttribute = (SqlServerParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(SqlServerParameterAttribute), true);

            if (statementParameterAttribute == null)
            {
                if (sqlServerParameterAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(SqlServerParameterAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetParameterValue(parameter,memberInfo, statementParameterAttribute, sqlServerParameterAttribute);


            if (string.IsNullOrWhiteSpace(statementParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }
            if (sqlServerParameterAttribute != null && string.IsNullOrWhiteSpace(sqlServerParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }

            return parameter;
        }
    }
}
