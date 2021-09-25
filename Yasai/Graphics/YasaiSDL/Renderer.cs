using System;
using static SDL2.SDL;

namespace Yasai.Graphics.YasaiSDL
{
    public class Renderer
    {
        private IntPtr renderer;
        
        // TODO: move all render functions to this class

        public Renderer(Window window)
        {
            renderer = SDL_CreateRenderer(window.GetPtr(), -1,
                SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (renderer == IntPtr.Zero)
                Console.WriteLine($"error on renderer creation: {SDL_GetError()}");
        }

        // wrap some functions
        public void Clear() => SDL_RenderClear(renderer);
        public void Present() => SDL_RenderPresent(renderer);
        public void SetDrawColor(byte r, byte g, byte b, byte a) => SDL_SetRenderDrawColor(renderer,r, g, b, a);

        public IntPtr GetPtr() => renderer;
    }
}