namespace Yasai
{
    /// <summary>
    /// Whether something belongs to a hierachy
    /// </summary>
    public interface IHierarchical
    {
        bool IgnoreHierachy { get; }
    }
}