using System;
using System.Reflection;

namespace Titan
{
    public static class AttributeException
    {
        #region exception
        public static InvalidOperationException AttributeNotFoundException(Type type, Type attributeType)
        {
            return new InvalidOperationException(string.Format("类型{0}缺少{1}标注", type, attributeType));
        }
        public static InvalidOperationException AttributeNotFoundException(MemberInfo memberInfo, Type attributeType)
        {
            return new InvalidOperationException(string.Format("类型{0}的属性{1}缺少{2}标注", memberInfo.ReflectedType, memberInfo, attributeType));
        }
        public static InvalidOperationException TableAttributeNotFoundException(Type type, Type attributeType)
        {
            return new InvalidOperationException(string.Format("类型{0}有{2}标注但是缺少TableAttribute", type, attributeType));
        }
        public static InvalidOperationException StatementAttributeNotFoundException(Type type, Type attributeType)
        {
            return new InvalidOperationException(string.Format("类型{0}有{2}标注但是缺少StatementAttribute", type, attributeType));
        }
        public static InvalidOperationException ColumnAttributeNotFoundException(MemberInfo memberInfo, Type attributeType)
        {
            return new InvalidOperationException(string.Format("类型{0}的属性{1}有{2}标注但是缺少ColumnAttribute", memberInfo.ReflectedType, memberInfo, attributeType));
        }
        #endregion
    }
}
