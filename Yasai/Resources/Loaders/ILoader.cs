namespace Yasai.Resources.Loaders
{
    public interface ILoader<T>
    {
        string[] FileTypes { get; }
        T GetResource(string path);
    }
}