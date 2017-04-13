
namespace Titan
{
    public interface IRelationColumn
    {
        string PropertyName { get; set; } 
        string ForeignPropertyName { get; set; } 
        string ColumnName { get; set; } 
        string ForeignColumnName { get; set; }
    }
}
