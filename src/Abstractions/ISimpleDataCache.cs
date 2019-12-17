namespace RruleTool.Abstractions
{
    public interface ISimpleDataCache
    {
        void Set(string key, object value);

        bool TryGet(string key, out object value);
    }
}
