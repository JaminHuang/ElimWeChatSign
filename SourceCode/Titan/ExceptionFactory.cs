using System;

namespace Titan
{
    internal static class ExceptionFactory
    {


        public static InvalidOperationException FullTextSearchNotSupported(string propertyName)
        {
            return new InvalidOperationException(string.Format("属性{0}不支持全文索引或缺少FullTextSearch标注", propertyName));
        }

        public static NotSupportedException NotSupported(string message)
        {
            return new NotSupportedException(message);
        }
         
         
    }
}
