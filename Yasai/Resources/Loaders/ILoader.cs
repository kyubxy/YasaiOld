namespace Yasai.Resources.Loaders
{
    public interface ILoader
    {
        string[] FileTypes { get; }
        ILoadArgs DefaultArgs { get; }
        Resource GetResource(Game game, string path, ILoadArgs args);
    }
}
