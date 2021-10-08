using System;

namespace Yasai.Resources
{
    public interface ILoad : IDependencyHolder
    {
        bool Loaded { get; }
        
        /// <summary>
        /// Called on resource load
        /// </summary>
        /// <param name="cache"></param>
        void Load(ContentCache cache);

        void LoadComplete();
    }
}