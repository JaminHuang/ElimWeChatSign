using System.Data;
using Titan.ExpressionAnalyse;

namespace Titan
{
    public interface IObjectAccessor
    {
        object Get(object entity, string propertyName);
        void Set(object entity, string propertyName, object value);
        object CreateInstance(object entityType);


        void Fill(ISqlProvider sqlProvider,IMappingProvider mappingProvider, IDataReader reader, object entity, ObjectFiller objectFiller);
    }
}
