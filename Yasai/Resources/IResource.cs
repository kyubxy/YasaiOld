using System;

namespace Yasai.Resources
{
    public interface IResource 
    {
        IntPtr Handle { get; }
    }
}