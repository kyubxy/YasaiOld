using System;
using System.Resources;
using SDL2;
using Yasai.Graphics.Layout;
using Yasai.Graphics.YasaiSDL;
using Yasai.Resources;


namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : IDisposable
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; } 
        
        private bool quit; 
        SDL.SDL_Event e;
        
        private string title = "Yasai";

        protected ScreenManager ScreenMgr;
        protected Screen Children;
        protected ContentStore Content;

        public Game()
        {
            // initialise
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
                Console.WriteLine($"error on startup: {SDL.SDL_GetError()}");
            
            ScreenMgr = new ScreenManager(Children = new Screen());
        }

        public void Run()
        {
            Window = new Window(title);
            Renderer = new Renderer(Window);
            Content = new ContentStore(this);
           
            Load();
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
                SDL.SDL_RenderClear(Renderer.GetPtr());
                Draw(Renderer.GetPtr());
                SDL.SDL_RenderPresent(Renderer.GetPtr());
                SDL.SDL_SetRenderDrawColor(Renderer.GetPtr(), 0, 0, 0, 255);
            }
        }

        public virtual void Load()
        {
            ScreenMgr.Load(Content);
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