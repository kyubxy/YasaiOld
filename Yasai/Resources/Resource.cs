using System;

namespace Yasai.Resources
{
    public abstract class Resource : IDisposable
    {
        public IntPtr Handle { get; protected set; }
        public string Path { get; private set; }
        public IResourceArgs Args { get; private set; }
        
        public Resource(IntPtr h, string p, IResourceArgs args)
        {
            Handle = h;
            Path = p;
            Args = args;
        }

        public virtual void Dispose()
        {
        }
    }
}
