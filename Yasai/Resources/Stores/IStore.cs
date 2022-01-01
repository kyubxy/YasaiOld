namespace Yasai.Resources.Stores
{
    public interface IStore 
    {
        string Root { get; }
        StorePrefs Prefs { get; }
        string[] FileTypes { get; }
        IResourceArgs DefaultArgs { get; }
    }
}