using System;
using System.Collections.Generic;
using System.Linq;

namespace Yasai.Structures
{
    public class DependencyCache
    {
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

        private Dictionary<Identifier, object> resolutionTable;

        public DependencyCache()
        {
            resolutionTable = new Dictionary<Identifier, object>();
        }

        /// <summary>
        /// Register a type for injection later down the line
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        public void Register<T>(object item, string name = "default")
        {
            resolutionTable[new Identifier(typeof(T), name)] = item;
        }

        /// <summary>
        /// Get a dependency that was previously registered
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="UnresolvableException"></exception>
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