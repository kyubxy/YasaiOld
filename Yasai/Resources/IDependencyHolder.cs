namespace Yasai.Resources
{
    // I really dont have a better name for this
    public interface IDependencyHolder
    {
        DependencyHandler DependencyHandler { get; set; }
    }
}