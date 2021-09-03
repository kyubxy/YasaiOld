using System;
using SDL2;

namespace Yasai.Graphics.YasaiSDL
{
    public class Window
    {
        private IntPtr window;

        public Window(string title)
        {
            window = SDL.SDL_CreateWindow(title, 50, 50, 1366, 768, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            if (window == IntPtr.Zero)
                Console.WriteLine($"error on window creation: {SDL.SDL_GetError()}");
        }

        public IntPtr GetPtr() => window;
    }
}