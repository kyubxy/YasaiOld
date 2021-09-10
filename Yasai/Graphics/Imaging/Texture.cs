using System;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Texture : IResource
    {
        // texture -> Draw this!!
        public IntPtr Handle { get; set; }
    }
}