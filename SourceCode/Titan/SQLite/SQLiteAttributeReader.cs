using System;
using System.Reflection;

namespace Titan.SQLite
{
    internal class SQLiteAttributeReader:AttributeReaderBase<
        TableBase,
        ColumnWithIdentity,
        StatementBase,
        ParameterBase,
        SQLiteTableAttribute,
        SQLiteColumnAttribute,
        SQLiteStatementAttribute,
        SQLiteParameterAttribute>
    {
        public override IColumn ReadColumnAttribute(MemberInfo memberInfo)
        {
            ColumnWithIdentity column = new ColumnWithIdentity();

            ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute), true);
            SQLiteColumnAttribute sqliteColumnAttribute = (SQLiteColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(SQLiteColumnAttribute), true);

            if (columnAttribute == null)
            {
                if (sqliteColumnAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(SQLiteColumnAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetColumnValue(column,memberInfo, columnAttribute, sqliteColumnAttribute);



            if (string.IsNullOrWhiteSpace(columnAttribute.ColumnName)
                && (
                    sqliteColumnAttribute == null ||
                    (sqliteColumnAttribute != null && string.IsNullOrWhiteSpace(sqliteColumnAttribute.ColumnName))
                    )
                )
            {
                //针对sqlServer,如果某个属性的ColumnAttribute.ColumnName为空，则自动在字段名加[]
                column.ColumnName = "[" + memberInfo.Name + "]";
            }
            //去除转义
            if (column.OutputAlias.StartsWith("[")) column.OutputAlias = column.OutputAlias.Substring(1, column.OutputAlias.Length - 1);
            if (column.OutputAlias.EndsWith("]")) column.OutputAlias = column.OutputAlias.Substring(0, column.OutputAlias.Length - 1);


            if (sqliteColumnAttribute != null)
            {
                column.IsIdentity = sqliteColumnAttribute.IsIdentity;
                if (column.IsIdentity) column.InsertBehavior.Generate = false;//如果是IsIdentity则强制不要在Insert语句中包含此列
                if (columnAttribute.ReturnAfterInsert == AttributeBoolean.Null && column.IsIdentity)
                {
                    column.ReturnAfterInsert = true;//如果ReturnAfterInsert=null则默认会取回自动增长的值
                }
            }
            return column;
        }

        public override IParameter ReadParameterAttribute(MemberInfo memberInfo)
        {
            ParameterBase parameter = new ParameterBase();

            ParameterAttribute statementParameterAttribute = (ParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ParameterAttribute), true);
            SQLiteParameterAttribute sqliteParameterAttribute = (SQLiteParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(SQLiteParameterAttribute), true);

            if (statementParameterAttribute == null)
            {
                if (sqliteParameterAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(SQLiteParameterAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetParameterValue(parameter, memberInfo, statementParameterAttribute, sqliteParameterAttribute);


            if (string.IsNullOrWhiteSpace(statementParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }
            if (sqliteParameterAttribute != null && string.IsNullOrWhiteSpace(sqliteParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }

            return null;
        }
    }
}
