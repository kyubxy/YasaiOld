using System;
using SDL2;
using Yasai.Graphics.Layout;
using Yasai.Graphics.YasaiSDL;


namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : IDisposable
    {
        public Window window { get; private set; }
        public Renderer renderer { get; private set; } 
        private bool quit; 
        SDL.SDL_Event e;
        
        private string title = "Yasai";

        public ScreenManager ScreenMgr;
        public Screen Children;

        public Game()
        {
            ScreenMgr = new ScreenManager(Children = new Screen());
            
            // initialise
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
                Console.WriteLine($"error on startup: {SDL.SDL_GetError()}");
        }

        public void Run()
        {
            window = new Window(title);
            renderer = new Renderer(window);
           
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
                SDL.SDL_RenderClear(renderer.GetPtr());
                Draw(renderer.GetPtr());
                SDL.SDL_RenderPresent(renderer.GetPtr());
                SDL.SDL_SetRenderDrawColor(renderer.GetPtr(), 0, 0, 0, 255);
            }
        }

        public virtual void Update()
        {
            ScreenMgr.Update();
        }

        // it's probably more efficient to pass around pointers than actual objects
        // can change this later though if need be
        public virtual void Draw(IntPtr ren)
        {
            ScreenMgr.Draw(ren);
        }

        public void Dispose()
        {
            ScreenMgr.Dispose();
            SDL.SDL_Quit();
        }
    }
}