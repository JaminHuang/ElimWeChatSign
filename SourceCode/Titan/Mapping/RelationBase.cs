namespace Titan
{
    /// <summary>
    /// 
    /// </summary>
    public class RelationBase :IRelation
    {

        public string PropertyName { get; set; }
        //public PropertyAdapter PropertyAdapter { get; set; } 
        public IRelationColumn[] RelationColumns { get; set; }
        public object ForeignEntityType { get; set; }


         

       
    } 
}
