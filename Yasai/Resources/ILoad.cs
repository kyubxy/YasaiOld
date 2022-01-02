using System;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Resources
{
    public interface ILoad 
    {
        bool Loaded { get; }

        /// <summary>
        /// Called on resource load
        /// </summary>
        /// <param name="container"></param>
        void Load(DependencyContainer container);
    }
}