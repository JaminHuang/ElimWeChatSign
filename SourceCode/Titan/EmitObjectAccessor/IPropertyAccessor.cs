namespace Titan.EmitObjectAccessor
{
    public interface IPropertyAccessor
    {
        object Get(object obj);
        void Set(object obj, object value);
    }
}
