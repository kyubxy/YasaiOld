using System;
using System.Collections;

namespace Yasai.Resources
{
    public interface ILoadArgs 
    {
    }

    /// <summary>
    /// A dummy type for resources with no load args
    /// </summary>
    public class EmptyLoadArgs : ILoadArgs
    {
        public ILoadArgs DefaultArgs => new EmptyLoadArgs();
    }
}