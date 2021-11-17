using System;

namespace Yasai.Resources
{
    public interface ILoad 
    {
        bool Loaded { get; }
        
        /// <summary>
        /// Called on resource load
        /// </summary>
        /// <param name="store"></param>
        void Load(ContentStore store);

        void LoadComplete();
    }
}