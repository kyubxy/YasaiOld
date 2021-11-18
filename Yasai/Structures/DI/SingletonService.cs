namespace Yasai.Structures.DI
{
    public struct SingletonService : IService
    {
        private object obj;
        public SingletonService(object obj) => this.obj = obj;
        public object GetService() => obj;
    }
}