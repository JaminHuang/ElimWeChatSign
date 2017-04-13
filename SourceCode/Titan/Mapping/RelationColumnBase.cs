 

namespace Titan
{
    public class RelationColumnBase:IRelationColumn
    {
        public string PropertyName { get; set; } 
        public string ForeignPropertyName { get; set; } 
        public string ColumnName { get; set; }
        public string ForeignColumnName { get; set; }

    }
}
