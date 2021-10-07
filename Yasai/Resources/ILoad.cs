using System;

namespace Yasai.Resources
{
    public interface ILoad : IDisposable
    {
        public bool Loaded { get; }
        /// <summary>
        /// Called on resource load
        /// </summary>
        /// <param name="cache"></param>
        public void Load(ContentCache cache);

        public void LoadComplete();
    }
}