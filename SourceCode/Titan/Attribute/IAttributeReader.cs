using System;
using System.Reflection;

namespace Titan
{
    public interface IAttributeReader
    {

        ITable ReadTableAttribute(Type entityType);

        IColumn ReadColumnAttribute(MemberInfo memberInfo);

        IRelation ReadRelationAttribute(MemberInfo memberInfo);

        IStatement ReadStatementAttribute(Type commandType);

        IParameter ReadParameterAttribute(MemberInfo memberInfo);

    }
}
