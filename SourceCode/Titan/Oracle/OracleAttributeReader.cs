using System;
using System.Data;
using System.Reflection;

namespace Titan.Oracle
{
    internal class OracleAttributeReader : AttributeReaderBase<
        TableBase,
        OracleColumn,
        StatementBase,
        ParameterBase,
        OracleTableAttribute,
        OracleColumnAttribute,
        OracleStatementAttribute,
        OracleParameterAttribute>
    {
        public override IColumn ReadColumnAttribute(MemberInfo memberInfo)
        {
            OracleColumn column = new OracleColumn();

            ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ColumnAttribute), true);
            OracleColumnAttribute oracleColumnAttribute = (OracleColumnAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(OracleColumnAttribute), true);

            if (columnAttribute == null)
            {
                if (oracleColumnAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(OracleColumnAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetColumnValue(column,memberInfo, columnAttribute, oracleColumnAttribute);


            if (column.OutputAlias.StartsWith("\"")) column.OutputAlias = column.OutputAlias.Substring(1, column.OutputAlias.Length - 1);
            if (column.OutputAlias.EndsWith("\"")) column.OutputAlias = column.OutputAlias.Substring(0, column.OutputAlias.Length - 1);

            if (oracleColumnAttribute != null)
            {
                column.Sequence = oracleColumnAttribute.Sequence;
                if (column.HasSequence)
                {
                    column.InsertBehavior.ValueBehavior = ValueBehavior.UseValueExpression;
                    column.InsertBehavior.ValueExpression = column.Sequence + ".nextval";
                    if (columnAttribute.ReturnAfterInsert == AttributeBoolean.Null)
                    {
                        column.ReturnAfterInsert = true;//如果ReturnAfterInsert=null则默认会取回自动增长的值
                    }
                }
            }
            return column;
        }

        public override IParameter ReadParameterAttribute(MemberInfo memberInfo)
        {
            OracleParameter parameter = new OracleParameter();
            //oracle有个特殊情况，如果有独到OracleCursorAttribute则立刻返回true 无需StatementParameterAttribute标记也可
            OracleCursorAttribute cursorAttribute = (OracleCursorAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(OracleCursorAttribute), true);
            if (cursorAttribute != null)
            {
                parameter.ParameterName = cursorAttribute.ParameterName;
                parameter.Direction = ParameterDirection.Output;
                parameter.ParameterName = string.IsNullOrWhiteSpace(cursorAttribute.ParameterName) ? memberInfo.Name : parameter.ParameterName = cursorAttribute.ParameterName;
                //parameter.PropertyAdapter = new PropertyAdapter(memberInfo);
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
                parameter.IsCursor = true;
                return parameter;
            }



            ParameterAttribute statementParameterAttribute = (ParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ParameterAttribute), true);
            OracleParameterAttribute oracleParameterAttribute = (OracleParameterAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(OracleParameterAttribute), true);

            if (statementParameterAttribute == null)
            {
                if (oracleParameterAttribute != null)
                {
                    throw AttributeException.ColumnAttributeNotFoundException(memberInfo, typeof(OracleParameterAttribute));
                }
                else
                {
                    return null;
                }
            }

            
            SetParameterValue(parameter,memberInfo, statementParameterAttribute, oracleParameterAttribute);


            return parameter;
        }
    }
}
