using System;
using System.Collections;

namespace Yasai.Resources
{
    public interface IResourceArgs 
    {
    }

    /// <summary>
    /// A dummy type for resources with no load args
    /// </summary>
    public class EmptyResourceArgs : IResourceArgs
    {
        public IResourceArgs DefaultArgs => new EmptyResourceArgs();
    }
}