using System;
using SDL2;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.Graphics.Primitives
{
    /// <summary>
    /// Texture based box. Although this primitive allows for more transformation options,
    /// <see cref="PrimitiveBox"/> may help performance-wise under intensive use
    /// </summary>
    public class Box : Sprite
    {
        public Box()
        {
            
        }

        public override void Load(ContentCache cache)
        {
            // generate a blank texture
            IntPtr renderer = cache.Game.Renderer.GetPtr();
            
            Texture blank = new Texture(SDL.SDL_CreateTexture(renderer,
                SDL.SDL_PIXELFORMAT_RGBA8888, (int) SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, 1, 1));

            // draw white on the texture
            SDL.SDL_SetRenderTarget(renderer, blank.Handle);
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

            CurrentTexture = blank;
            
            CenterToCurrentTex();
        }

        public override void Dispose()
        {
            base.Dispose();
            CurrentTexture.Dispose();
        }
    }
}