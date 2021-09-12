using System;
using Yasai.Resources;

namespace Yasai.Graphics.Text
{
    public class SpriteFont : IResource
    {
        public IntPtr Handle { get; set; }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}