using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;

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
                
                if (SDL.SDL_QueryTexture(Handle, out _, out _, out w, out h) != 0)
                    throw new Exception(SDL.SDL_GetError());

                return new Vector2(w, h);
            }
        }

        public Texture(IntPtr ptr, string path = "") : base(ptr, path)
        { }
        
        public Texture() {}

        public void Dispose()
        {
            SDL.SDL_DestroyTexture(Handle);
        }
    }
}
