namespace Yasai.Structures.DI
{
    public interface ITransientDependency<T>
    {
        T GetNewService();
    }
}