namespace Yasai.Resources.Loaders
{
    public interface ILoader
    {
        string[] FileTypes { get; }
        IResource GetResource(Game game, string path, ILoadArgs args);
    }
}