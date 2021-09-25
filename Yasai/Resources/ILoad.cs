using System;

namespace Yasai.Resources
{
    public interface ILoad : IDisposable
    {
        public bool Loaded { get; }
        /// <summary>
        /// Called before load, state what needs to be loaded here
        /// </summary>
        /// <param name="cache"></param>
        public void Start(ContentCache cache);
        
        /// <summary>
        /// Called on resource load
        /// </summary>
        /// <param name="cache"></param>
        public void Load(ContentCache cache);
    }
}