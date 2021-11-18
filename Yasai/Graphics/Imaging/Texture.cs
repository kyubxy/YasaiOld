using System;
using System.Numerics;

using Yasai.Resources;

using static SDL2.SDL;

namespace Yasai.Graphics.Imaging
{
    public class Texture : Resource
    {
        // texture -> Draw using Handle!! (inherited from Resource)

        public Vector2 Size
        {
            get
            {
                int w, h;
                
                if (SDL_QueryTexture(Handle, out _, out _, out w, out h) != 0)
                    throw new Exception(SDL_GetError());

                return new Vector2(w, h);
            }
        }

        public Texture(IntPtr ptr, string path = "") : base(ptr, path, new EmptyResourceArgs())
        { }
        
        public override void Dispose()
        {
            SDL_DestroyTexture(Handle);
        }
    }
}
