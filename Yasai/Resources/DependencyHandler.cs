using System;
using System.Collections.Generic;
using Yasai.Graphics;

namespace Yasai.Resources
{
    /// <summary>
    /// Handles dependency injection between <see cref="Drawable"/>s
    /// Allows information to propagate down the scene graph. Cannot go upwards.
    /// </summary>
    public class DependencyHandler
    {
        private Dictionary<string, ITracable> cache;

        public DependencyHandler() => cache = new Dictionary<string, ITracable>();

        public DependencyHandler(DependencyHandler d)
        {
            cache = d.GetCache();
        }

        public Dictionary<string, ITracable> GetCache() => new (cache);

        /// <summary>
        /// Add something to dependency cache and ensure its traceable
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="context"></param>
        public void Store<T>(Tracable<T> dependency, string context = "def")
        {
            dependency.Change += value => OnChange(value, context);
            cache[context] = dependency;
        }

        void OnChange<T>(T value, string context)
        {
            ITracable t = new Tracable<T>(value);
            cache[context] = t;
        }

        /// <summary>
        /// Retrieve something from dependency cache
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public Tracable<T> Retrieve<T>(string context = "def")
        {
            if (!cache.ContainsKey(context))
                throw new KeyNotFoundException($"no such context {context} in cache");
            
            var dependency = cache[context];
            
            if (dependency.ValueType != typeof(T))
                throw new InvalidCastException($"cannot find a dependency with context {context}");
            
            return (Tracable<T>)dependency;
        }
    }
}