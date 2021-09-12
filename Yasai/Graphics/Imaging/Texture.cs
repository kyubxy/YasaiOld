using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Texture : IResource
    {
        // texture -> Draw this!!
        public IntPtr Handle { get; set; }

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

        public void Dispose()
        {
            SDL.SDL_DestroyTexture(Handle);
        }
    }
}