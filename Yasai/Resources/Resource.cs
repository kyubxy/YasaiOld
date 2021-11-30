using System;

namespace Yasai.Resources
{
    public interface IResource : IDisposable
    {
        string Path { init; get; }
        IResourceArgs Args { init; get; }
    }
    
    public abstract class Resource<T> : IDisposable
    {
        //public IntPtr Handle { get; protected set; }
        public T Handle { init; get; }
        public string Path { init; get; }
        public IResourceArgs Args { init; get; }

        public Resource(T handle, string p, IResourceArgs args)
        {
            Handle = handle;
            Path = p;
            Args = args;
        }
        
        public Resource() {}

        public virtual void Dispose()
        { }
    }
}
