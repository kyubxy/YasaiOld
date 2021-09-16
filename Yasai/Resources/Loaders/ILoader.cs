namespace Yasai.Resources.Loaders
{
    public interface ILoader
    {
        string[] FileTypes { get; }
        Resource GetResource(Game game, string path, ILoadArgs args);
    }
}
