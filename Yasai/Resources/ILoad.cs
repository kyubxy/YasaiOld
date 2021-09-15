using System;

namespace Yasai.Resources
{
    public interface ILoad : IDisposable
    {
        public bool Loaded { get; }
        public void Load(ContentCache cache);
    }
}