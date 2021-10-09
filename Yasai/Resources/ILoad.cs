using System;

namespace Yasai.Resources
{
    public interface ILoad 
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