using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    public class GroupExpression:IGroupExpression
    {
         #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupExpression()
        {
            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">输出属性的名称</param> 
        public GroupExpression(string propertyName)
        {
            PropertyName = propertyName; 
        } 

        #endregion

        /// <summary>
        /// 输出的属性的名称
        /// </summary> 
        [DataMember]
        public string PropertyName { get; set; }

 
         
        public override string ToString()
        {
            return PropertyName;
        }
    }
}
