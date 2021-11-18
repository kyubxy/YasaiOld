using System;
using Yasai.Resources;

namespace Yasai.Audio
{
    /// <summary>
    /// Resource for any sound to be made
    /// </summary>
    public class Audio : Resource
    {
        public Audio(IntPtr h, string p, IResourceArgs args) : base(h, p, args)
        {
        }
    }
}