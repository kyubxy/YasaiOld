using System;

namespace Yasai.Resources
{
    public abstract class Resource : IDisposable
    {
        public IntPtr Handle { get; protected set; }
        public string Path { get; private set; }
        
        public Resource(IntPtr h, string p)
        {
            Handle = h;
            Path = p;
        }
    }
}
