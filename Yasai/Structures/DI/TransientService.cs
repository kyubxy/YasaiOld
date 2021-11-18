namespace Yasai.Structures.DI
{
    public struct TransientService<T> : IService
        where T : ITransientDependency<T>
    {
        private T obj;
        public TransientService(T obj) => this.obj = obj;
        public object GetService() => obj.GetNewService();
    }
}