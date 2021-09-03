using System;
using SDL2;

namespace Yasai.Graphics.YasaiSDL
{
    public class Renderer
    {
        private IntPtr renderer;

        public Renderer(Window window)
        {
            renderer = SDL.SDL_CreateRenderer(window.GetPtr(), -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (renderer == IntPtr.Zero)
                Console.WriteLine($"error on renderer creation: {SDL.SDL_GetError()}");
        }

        public IntPtr GetPtr() => renderer;
    }
}