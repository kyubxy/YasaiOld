using System;

namespace Yasai.Resources
{
    public interface IResource : IDisposable
    {
        string Path { init; get; }
        IResourceArgs Args { init; get; }
    }
    
    public abstract class Resource : IResource
    {
        public string Path { init; get; }
        public IResourceArgs Args { init; get; }

        public Resource(string p, IResourceArgs args)
        {
            Path = p;
            Args = args;
        }
        
        public Resource() {}

        public virtual void Dispose()
        { }
    }
}
