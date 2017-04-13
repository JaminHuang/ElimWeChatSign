using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Titan.ExpressionAnalyse;

namespace Titan.EmitObjectAccessor
{
    public class ObjectAccessor : IObjectAccessor
    {

        #region 单例模式
        private static readonly ObjectAccessor instance = new ObjectAccessor();
        public static ObjectAccessor Instance
        {
            get
            {
                return instance;
            }
        }

        private ObjectAccessor()
        {
        }
        #endregion

        #region 缓存
        private static readonly Dictionary<Type, Dictionary<string, IPropertyAccessor>> caches = new Dictionary<Type, Dictionary<string, IPropertyAccessor>>();
        #endregion

        private IPropertyAccessor GetPropertyAccessor(object entityType, string propertyName)
        {
            Type type = (Type)entityType;
            if (!caches.ContainsKey(type))
            {
                lock (caches)
                {
                    if (!caches.ContainsKey(type))
                    {
                        caches.Add(type,new Dictionary<string, IPropertyAccessor>());
                    }
                }
            }

            Dictionary<string, IPropertyAccessor> dic = caches[type];

            if (!dic.ContainsKey(propertyName))
            {
                lock (caches)
                {
                    if (!dic.ContainsKey(propertyName))
                    {
                        MemberInfo memberInfo = type.GetMember(propertyName)[0];
                        IPropertyAccessor accessor;
                        if (memberInfo is FieldInfo)
                        { 
                            accessor = new FieldAccessor(memberInfo as FieldInfo);
                        }
                        else
                        {
                            accessor = new PropertyAccessor(memberInfo as PropertyInfo);
                        }
                        dic.Add(propertyName, accessor);
                    }
                }
            }
            return dic[propertyName];
        }


        public object Get(object entity, string propertyName)
        {
            IPropertyAccessor accessor = GetPropertyAccessor(entity.GetType(), propertyName);
            return accessor.Get(entity);
        }

        public void Set(object entity, string propertyName, object value)
        {
            IPropertyAccessor accessor = GetPropertyAccessor(entity.GetType(), propertyName);
            accessor.Set(entity, value);
        }

        public object CreateInstance(object entityType)
        {
            return Activator.CreateInstance((Type)entityType);
        }

        public void Fill(ISqlProvider sqlProvider,IMappingProvider mappingProvider,IDataReader reader, object entity, ObjectFiller objectFiller)
        {
            foreach (PropertyFiller propertyFiller in objectFiller.PropertyFillers)
            {
                //Console.WriteLine(propertyFiller.Column.ColumnName + ":" + propertyFiller.OutputColumnIndex + ":" + propertyFiller.Column.PropertyAdapter.PropertyName + ":" + propertyFiller.Column.PropertyAdapter.PropertyType);
                //propertyFiller.Column.FillPropertyValue(entityObject, dataReader, propertyFiller.OutputColumnIndex);
                //propertyFiller.getPropertyAdapter().setPropertyValue(object, resultSet.getObject(propertyFiller.getOutputColumnIndex()));

                //为值类型赋值null会出问题
                object value = sqlProvider.ConvertDbValue(reader[propertyFiller.ColumnIndex], propertyFiller.PropertyType);
                if (!(propertyFiller.PropertyType.IsValueType && value == null))
                {
                    Set(entity,propertyFiller.PropertyName, value);
                }
            }
            foreach (ObjectFiller f in objectFiller.ObjectFillers)
            {
                //如果已经有实例，则不需要实例化
                object existsInstance = Get(entity,f.PropertyName);
                if (existsInstance == null)
                {
                    existsInstance = CreateInstance(f.EntityType);
                    Set(entity, f.PropertyName, existsInstance); 
                }
                Fill(sqlProvider, mappingProvider,reader, existsInstance, f); 
            }
        }
    }
}
