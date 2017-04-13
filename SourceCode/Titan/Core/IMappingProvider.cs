namespace Titan
{
    public interface IMappingProvider
    {
        object GetCommandTypeByInstance(object entity);
        object GetEntityTypeByInstance(object entity);

        ITable GetTable(object entityType);

        IStatement GetStatement(object commandType);

    }
}
