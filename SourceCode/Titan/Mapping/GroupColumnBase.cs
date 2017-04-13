using System;

namespace Titan
{

    /// <summary>
    /// 
    /// </summary>
    public class GroupColumnBase : IGroupColumn
    {

        public GroupFunction GroupFunction { get; set; }
        public string OriginalPropertyName { get; set; }
        public string PropertyName { get; set; }


        //public PropertyAdapter PropertyAdapter { get; set; }
        public Type PropertyType { get; set; }
         

    }

}
