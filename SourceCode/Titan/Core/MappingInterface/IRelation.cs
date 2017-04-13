namespace Titan
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRelation
    {
        string PropertyName { get; set; }
        //PropertyAdapter PropertyAdapter { get; set; } 
        IRelationColumn[] RelationColumns { get; set; }

        object ForeignEntityType { get; set; }

         
    } 
}
