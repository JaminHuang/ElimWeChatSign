using System;
using System.Reflection;

namespace Titan.MySql
{
    internal class MySqlAttributeReader : AttributeReaderBase<
        TableBase,
        ColumnWithIdentity,
        StatementBase,
        ParameterBase,
        MySqlTableAttribute,
        MySqlColumnAttribute,
        MySqlStatementAttribute,
        MySqlParameterAttribute>
    {
        public override IColumn ReadColumnAttribute(MemberInfo memberInfo)
        {
            ColumnWithIdentity column = new ColumnWithIdentity();

            ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute), true);
            MySqlColumnAttribute mySqlColumnAttribute = (MySqlColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(MySqlColumnAttribute), true);

            if (columnAttribute == null)
            {
                if (mySqlColumnAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(MySqlColumnAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetColumnValue(column,memberInfo, columnAttribute, mySqlColumnAttribute);


            if (column.OutputAlias.StartsWith("`")) column.OutputAlias = column.OutputAlias.Substring(1, column.OutputAlias.Length - 1);
            if (column.OutputAlias.EndsWith("`")) column.OutputAlias = column.OutputAlias.Substring(0, column.OutputAlias.Length - 1);

            if (mySqlColumnAttribute != null)
            {
                column.IsIdentity = mySqlColumnAttribute.IsIdentity;
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
            MySqlParameterAttribute mySqlParameterAttribute = (MySqlParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(MySqlParameterAttribute), true);

            if (statementParameterAttribute == null)
            {
                if (mySqlParameterAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(MySqlParameterAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetParameterValue(parameter,memberInfo, statementParameterAttribute, mySqlParameterAttribute);


            if (string.IsNullOrWhiteSpace(statementParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }
            if (mySqlParameterAttribute != null && string.IsNullOrWhiteSpace(mySqlParameterAttribute.ParameterName))
            {
                parameter.ParameterName = "@" + memberInfo.Name;
            }

            return parameter;
        }
    }
}
