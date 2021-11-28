using System;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.YasaiSDL;
using Yasai.Resources;
using Yasai.Structures;
using Yasai.Structures.DI;
using static SDL2.SDL;

namespace Yasai.Graphics.Primitives
{
    /// <summary>
    /// Texture based box. Although this primitive allows for more transformation options,
    /// <see cref="PrimitiveBox"/> may help performance-wise under intensive use
    /// </summary>
    public class Box : Sprite
    {
        public override void Load(DependencyContainer dependencies)
        {
            // generate a blank texture
           IntPtr renderer = dependencies.Resolve<Renderer>().GetPtr();   //store.Game.Renderer.GetPtr();
           
           Texture blank = new Texture(SDL_CreateTexture(renderer,
               SDL_PIXELFORMAT_RGBA8888, (int) SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, 1, 1));

           // draw white on the texture
           SDL_SetRenderTarget(renderer, blank.Handle);
           SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
           SDL_RenderClear(renderer);
           SDL_SetRenderTarget(renderer, IntPtr.Zero);

           CurrentTexture = blank;
           
           CenterToCurrentTex();
        }
    }
}