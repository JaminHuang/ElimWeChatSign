using System;
using System.Collections.Generic;

namespace Titan
{
    static class Extension
    {
        public static void FillPropertySets(this IObjectAccessor objectAccessor,IMappingProvider mappingProvider, object entityType, object entity, IDictionary<string, object> propertySets, IEnumerable<string> propertyNames,IEnumerable<string> excludePropertyNames)
        {
            HashSet<string> exclude = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (excludePropertyNames != null)
            {
                exclude = new HashSet<string>(excludePropertyNames, StringComparer.OrdinalIgnoreCase);
            }
            ITable table=mappingProvider.GetTable(entityType);
            foreach (string propertyName in propertyNames)
            {
                if (!exclude.Contains(propertyName))
                {
                    string propertyName2 = table.Columns[propertyName].PropertyName;
                    if (propertySets.ContainsKey(propertyName2))
                    {
                        propertySets[propertyName2] = objectAccessor.Get(entity, propertyName2);
                    }
                    else
                    {
                        propertySets.Add(propertyName2, objectAccessor.Get(entity, propertyName2));
                    }
                }
            }
        }
        public static IDictionary<string, object> ToPropertySets(this IObjectAccessor objectAccessor, IMappingProvider mappingProvider, object entityType, object entity, IEnumerable<string> propertyNames)
        {
            Dictionary<string, object> propertySets = new Dictionary<string, object>();
            objectAccessor.FillPropertySets(mappingProvider,entityType, entity, propertySets, propertyNames, null);
            return propertySets;
        }
        public static IDictionary<string, object> ToPropertySets(this IObjectAccessor objectAccessor, IMappingProvider mappingProvider, object entityType, object entity, IEnumerable<string> propertyNames, IEnumerable<string> excludePropertyNames)
        {
            Dictionary<string, object> propertySets = new Dictionary<string, object>();
            objectAccessor.FillPropertySets(mappingProvider,entityType, entity, propertySets, propertyNames, excludePropertyNames);
            return propertySets;
        }
    }
}
