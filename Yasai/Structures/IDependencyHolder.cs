namespace Yasai.Structures
{
    public interface IDependencyHolder 
    {
        DependencyCache DependencyCache { get; }
        void LinkDependencies(Linkable<DependencyCache> parent);
    }
}