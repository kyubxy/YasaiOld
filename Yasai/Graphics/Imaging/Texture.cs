using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Texture : IResource
    {
        // texture -> Draw this!!
        public IntPtr Handle { get; }

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

        public Texture(IntPtr ptr) => Handle = ptr;
        
        public Texture () {}

        public void Dispose()
        {
            SDL.SDL_DestroyTexture(Handle);
        }
    }
}