using System.Collections.Generic;

namespace Yasai.Structures.DI
{
    public class DependencyContainer
    {
        private readonly Dictionary<Identifier, IService> resolutionTable;

        public DependencyContainer()
            => resolutionTable = new Dictionary<Identifier, IService>();

        /// <summary>
         /// Register a type for injection later down the line with a singleton lifetime
         /// </summary>
         /// <param name="item">the item to register</param>
         /// <param name="name">the name of the item, this is set to "default" by default</param>
         /// <typeparam name="T">type to register as</typeparam>
        public void Register<T>(object item, string name = "default")
            => resolutionTable[new Identifier(typeof(T), name)] = new SingletonService(item);

        /// <summary>
        /// Register a type for injection later down the line with a transient lifetime
        /// </summary>
        /// <param name="item">the item to register</param>
        /// <param name="name">the name of the item, this is set to "default" by default</param>
        /// <typeparam name="T">type to register as</typeparam>
        /// <typeparam name="TI"></typeparam>
        public void RegisterTransient<T, TI>(TI item, string name = "default") 
            where TI : ITransientDependency<TI>
                => resolutionTable[new Identifier(typeof(T), name)] = new TransientService<TI>(item);

        /// <summary>
         /// Get a dependency that was previously registered
         /// </summary>
         /// <param name="name">name it was registered with (if applicable)</param>
         /// <typeparam name="T">type to resolve</typeparam>
         /// <returns></returns>
         /// <exception cref="UnresolvableException">an unresolvable identifier was given</exception>
        public T Resolve<T>(string name = "default")
        {
            var identifier = new Identifier(typeof(T), name);
            if (!resolutionTable.ContainsKey(identifier))
                throw new UnresolvableException
                    ($"no such identifier with type {typeof(T)} and name {name} has been registered");

            return (T)resolutionTable[identifier].GetService();
        }
    }
}