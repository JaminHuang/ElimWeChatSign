using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    public class OutputExpression:IOutputExpression
    {
         #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OutputExpression()
        {
            Property = new PropertyExpression();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        public OutputExpression(string propertyName)
        {
            Property = new PropertyExpression(propertyName);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="alias"></param>
        public OutputExpression(string propertyName, string alias)
        {
            Property = new PropertyExpression(propertyName);
            Alias = alias;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="groupFunction"></param>
        public OutputExpression(string propertyName, GroupFunction groupFunction)
        {
            Property = new PropertyExpression(propertyName, groupFunction);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param>
        /// <param name="groupFunction"></param>
        /// <param name="alias"></param>
        public OutputExpression(string propertyName, GroupFunction groupFunction, string alias)
        {
            Property = new PropertyExpression(propertyName, groupFunction);
            Alias = alias;
        }

        #endregion

        /// <summary>
        /// 输出的属性的名称
        /// </summary> 
        [DataMember]
        public PropertyExpression Property { get; set; }



        /// <summary>
        /// 排序类型，升序或者降序
        /// </summary> 
        [DataMember]
        public string Alias { get; set; }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (((object) Property) == null)
            { 
                return Alias == null ? base.GetHashCode() : Alias.GetHashCode();
            }
            else if (Alias == null)
            {
                return (object) Property == null ? base.GetHashCode() : Property.GetHashCode();
            }
            else
            {
                return Property.GetHashCode() ^ Alias.GetHashCode();
            }
        }


        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Alias) ? Property.ToString() : Property + " " + Alias;
        }


        #region 显式接口
        IPropertyExpression IOutputExpression.Property
        {
            get
            {
                return Property;
            }
            set
            {
                Property=(PropertyExpression)value;
            }
        }
        #endregion
    }
}
