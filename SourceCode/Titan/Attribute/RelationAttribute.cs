using System;

namespace Titan
{

    /// <summary>
    /// 描述实体类外部关系对象属性的Attribute，各类数据库都有具有的特性都通过这个类描述。
    /// 只能用于类的Property或者Field上
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class RelationAttribute : Attribute
    { 
        public static readonly char[] SPLIT_CHARS_EQUALS = new char[] { '=' };
        public static readonly char[] SPLIT_CHARS_POINT = new char[] { '.' };
        /// <summary>
        /// 内部使用的一个类，描述与外键对象关联的属性名称
        /// </summary>
        public sealed class RelationProperty
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="propertyName">实体类中的属性名</param>
            /// <param name="foreignPropertyName">外部关系类中的属性名</param>
            public RelationProperty(string propertyName, string foreignPropertyName)
            {
                PropertyName = propertyName;//local
                ForeignPropertyName = foreignPropertyName;
            }


            /// <summary>
            /// 实体类中的属性名
            /// </summary>
            public string PropertyName { get; set; }


            /// <summary>
            /// 外部关系类中的属性名
            /// </summary>
            public string ForeignPropertyName { get; set; }
        }

        /// <summary>
        /// 构造函数,
        /// 支持的格式有：
        /// this.LivingCityId=foreign.CityId
        /// LivingCityId=CityId 
        /// this.CityId
        /// CityId
        /// </summary>
        public RelationAttribute(params string[] relationPropertys)
        {
            RelationPropertys = new RelationProperty[relationPropertys.Length];

            for (int i = 0; i < relationPropertys.Length; i++)
            {
                string relationProperty = relationPropertys[i];
                string[] relationPropertySplit = relationProperty.Split(SPLIT_CHARS_EQUALS, StringSplitOptions.RemoveEmptyEntries);
                if (relationPropertySplit.Length > 1)
                {
                    string[] leftSplit = relationPropertySplit[0].Split(SPLIT_CHARS_POINT, StringSplitOptions.RemoveEmptyEntries);
                    string[] rightSplit = relationPropertySplit[1].Split(SPLIT_CHARS_POINT, StringSplitOptions.RemoveEmptyEntries);
                    string left = leftSplit.Length > 1 ? leftSplit[1] : leftSplit[0];
                    string right = rightSplit.Length > 1 ? rightSplit[1] : rightSplit[0];
                    RelationPropertys[i] = new RelationProperty(left, right);
                }
                else
                {
                    string[] leftSplit = relationPropertySplit[0].Split(SPLIT_CHARS_POINT, StringSplitOptions.RemoveEmptyEntries);
                    string left = leftSplit.Length > 1 ? leftSplit[1] : leftSplit[0];
                    RelationPropertys[i] = new RelationProperty(left, left);
                }
            }
        }

        /// <summary>
        /// 属性关系
        /// </summary>
        public RelationProperty[] RelationPropertys { get; set; }


    }
}
