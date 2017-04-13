using System.Collections.Generic;

namespace Titan.ExpressionAnalyse
{
    public class ObjectFiller
    {

        /// <summary>
        /// 
        /// </summary>
        public object EntityType { get; set; }


        /// <summary>
        /// 例如Person.City中的City属性
        /// </summary>
        public string PropertyName { get; set; }
         
         
        /// <summary>
        /// 实体对象中包含的所有需要填充的关系对象，例如Person.City中的City对象
        /// </summary>
        public List<ObjectFiller> ObjectFillers { get; set; }


        /// <summary>
        /// 实体对象中包含的所有需要填充的属性，例如Person.City中的CityId属性
        /// </summary>
        public List<PropertyFiller> PropertyFillers { get; set; }



        //public void Fill(ISqlProvider sqlProvider, object entityObject, IDataReader dataReader)
        //{
        //    foreach (PropertyFiller propertyFiller in PropertyFillers)
        //    {
        //        //Console.WriteLine(propertyFiller.Column.ColumnName + ":" + propertyFiller.OutputColumnIndex + ":" + propertyFiller.Column.PropertyAdapter.PropertyName + ":" + propertyFiller.Column.PropertyAdapter.PropertyType);
        //        //propertyFiller.Column.FillPropertyValue(entityObject, dataReader, propertyFiller.OutputColumnIndex);
        //        //propertyFiller.getPropertyAdapter().setPropertyValue(object, resultSet.getObject(propertyFiller.getOutputColumnIndex()));

        //        //为值类型赋值null会出问题
        //        object value = sqlProvider.ConvertDbValue(dataReader[propertyFiller.OutputColumnIndex], propertyFiller.PropertyAdapter.PropertyType.Type);
        //        if (!(propertyFiller.PropertyAdapter.PropertyType.Type.IsValueType && value == null))
        //        {
        //            propertyFiller.PropertyAdapter.SetValue(entityObject, value);
        //        }
        //    }
        //    foreach (ObjectFiller objectFiller in ObjectFillers)
        //    {
        //        //如果已经有实例，则不需要实例化
        //        object existsInstance = objectFiller.PropertyAdapter.GetValue(entityObject);
        //        if (existsInstance == null)
        //        {
        //            existsInstance = Activator.CreateInstance(objectFiller.PropertyAdapter.PropertyType.Type);
        //            if (existsInstance is IDynamicEntity)
        //            {
        //                (existsInstance as IDynamicEntity).TypeName = objectFiller.PropertyAdapter.PropertyType.TypeName;
        //            }
        //            objectFiller.PropertyAdapter.SetValue(entityObject, existsInstance);
        //        } 
        //        objectFiller.Fill(sqlProvider, existsInstance, dataReader);
        //    }
        //}
    }
}
