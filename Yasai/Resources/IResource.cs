using System;

namespace Yasai.Resources
{
    public interface IResource : IDisposable
    {
        IntPtr Handle { get; }
    }
}