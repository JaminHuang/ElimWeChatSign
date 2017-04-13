using System;
using System.Collections.Generic;

namespace Titan
{
    public abstract class MappingProviderBase:IMappingProvider
    {

        private readonly IAttributeReader attributeReader;
        private readonly Dictionary<Type, ITable> tables = new Dictionary<Type, ITable>();//缓存,FullEntityName作为键，
        private readonly Dictionary<Type, IStatement> statements = new Dictionary<Type, IStatement>(); //缓存


        public abstract IAttributeReader CreateAttributeReader();


        protected MappingProviderBase()
        {
            attributeReader=CreateAttributeReader();
        }

        public object GetCommandTypeByInstance(object obj)
        {
            return obj.GetType();
        }

        public object GetEntityTypeByInstance(object obj)
        {
            return obj.GetType();
        }

        public ITable GetTable(object entityType)
        {
            Type type = (Type)entityType;
            if (!tables.ContainsKey(type))
            {
                lock (tables)
                {
                    if (!tables.ContainsKey(type))
                    {
                        ITable table = attributeReader.ReadTableAttribute(type);
                        if (table == null)
                        {
                            throw AttributeException.AttributeNotFoundException(type, typeof(TableAttribute));
                        }
                        //必须要查找出RelationColumn的ColumnName 
                        foreach (IRelation relation in table.Relations.Values)
                        {
                            foreach (IRelationColumn relationColumn in relation.RelationColumns)
                            {
                                string columnName = table.Columns[relationColumn.PropertyName].ColumnName;
                                Type foreignEntityType = (Type)(relation.ForeignEntityType);
                                ITable t = attributeReader.ReadTableAttribute(foreignEntityType);
                                if (t == null)
                                {
                                    throw AttributeException.AttributeNotFoundException(foreignEntityType, typeof(TableAttribute));
                                }
                                string foreignColumnName = t.Columns[relationColumn.ForeignPropertyName].ColumnName;
                                relationColumn.ColumnName = columnName;
                                relationColumn.ForeignColumnName = foreignColumnName;
                            }
                        }
                        table.CreateCache();
                        tables.Add(type, table);
                    }
                }
            }
            return tables[type];
        }

        public IStatement GetStatement(object commandType)
        {
            Type type = (Type)commandType;
            if (!statements.ContainsKey(type))
            {
                lock (statements)
                {
                    if (!statements.ContainsKey(type))
                    {
                        IStatement command = attributeReader.ReadStatementAttribute(type);
                        if (command == null)
                        {
                            throw AttributeException.AttributeNotFoundException(type, typeof(StatementAttribute));
                        }
                        command.CreateCache();
                        statements.Add(type, command);
                    }
                }
            }
            return statements[type];
        }
    }
}
