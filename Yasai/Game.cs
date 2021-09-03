using System;
using SDL2;
using Yasai.Graphics.Layout;


namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : IDisposable
    {
        public IntPtr Window; 
        public IntPtr Renderer;
        bool quit = false;
        SDL.SDL_Event e;
        
        private string title = "Yasai";

        public Game()
        {
            // initialise
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
                Console.WriteLine($"error on startup: {SDL.SDL_GetError()}");
        }

        public void Run()
        {
            Window = SDL.SDL_CreateWindow(title, 50, 50, 1366, 768, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            if (Window == IntPtr.Zero)
                Console.WriteLine($"error on window creation: {SDL.SDL_GetError()}");

            Renderer = SDL.SDL_CreateRenderer(Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (Renderer == IntPtr.Zero)
                Console.WriteLine($"error on renderer creation: {SDL.SDL_GetError()}");

            while (!quit)
            {
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    switch (e.type)
                    {
                        case (SDL.SDL_EventType.SDL_QUIT):
                            quit = true;
                            break;
                    }
                }

                Update();
                SDL.SDL_RenderClear(Renderer);
                Draw(Renderer);
                SDL.SDL_RenderPresent(Renderer);
                SDL.SDL_SetRenderDrawColor(Renderer, 0, 0, 0, 255);
            }
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(IntPtr renderer)
        {
        }

        public void Dispose()
        {
            SDL.SDL_Quit();
        }
    }
}