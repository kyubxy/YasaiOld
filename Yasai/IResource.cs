using System;

namespace Yasai
{
    public interface IResource : IDisposable
    {
        public bool Loaded { get; }
        public void Load();
    }
}