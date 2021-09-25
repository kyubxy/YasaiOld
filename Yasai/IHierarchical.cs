namespace Yasai
{
    /// <summary>
    /// Whether something belongs to a hierarchy
    /// </summary>
    public interface IHierarchical
    {
        bool IgnoreHierarchy { get; }
    }
}