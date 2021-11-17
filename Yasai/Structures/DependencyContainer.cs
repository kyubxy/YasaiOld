using System;
using System.Collections.Generic;

namespace Yasai.Structures
{
    public class DependencyContainer
    {
        #region extras
        class UnresolvableException : Exception
        {
            public UnresolvableException()
            { }
            
            public UnresolvableException(string message)
                : base(message) 
            { }

            public UnresolvableException(string message, Exception inner) 
                : base(message, inner)
            { }

        }

        readonly struct Identifier
        {
            public readonly Type Type;
            public readonly string Name;

            public Identifier(Type type, String name)
            {
                Type = type;
                Name = name;
            }

            public override string ToString() => "{" + $"Type: {Type}, Name: {Name}" + "}";
        }
        #endregion

        private Dictionary<Identifier, object> resolutionTable;

        public DependencyContainer()
        {
            resolutionTable = new Dictionary<Identifier, object>();
        }

        /// <summary>
        /// Register a type for injection later down the line
        /// </summary>
        /// <param name="item">the item to register</param>
        /// <param name="name">the name of the item, this is set to "default" by default</param>
        /// <typeparam name="T">type to register as</typeparam>
        public void Register<T>(object item, string name = "default")
        {
            resolutionTable[new Identifier(typeof(T), name)] = item;
        }

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
            {
                throw new UnresolvableException
                    ($"no such identifier with type {typeof(T)} and name {name} has been registered");
            }

            return (T)resolutionTable[identifier];
        }
    }
}