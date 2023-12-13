namespace Balda
{
    public interface IResourceLoader
    {
        T LoadLocal<T>(string fileName) where T : class;
    }
}