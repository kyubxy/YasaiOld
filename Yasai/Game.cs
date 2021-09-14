using System;
using SDL2;
using Yasai.Graphics.Layout;
using Yasai.Graphics.YasaiSDL;
using Yasai.Input;
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
        
        private string title = "Yasai";

        protected ScreenManager ScreenMgr;
        protected Screen Children;
        protected ContentStore Content;

        public Game()
        {
            // initialise
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
                Console.WriteLine($"error on startup: {SDL.SDL_GetError()}");

            SDL_ttf.TTF_Init();
            
            ScreenMgr = new ScreenManager(Children = new Screen());
        }

        public void Run()
        {
            Window = new Window(title);
            Renderer = new Renderer(Window);
            Content = new ContentStore(this);
           
            ScreenMgr.Load(Content);
            Load();
            
            while (!quit)
            {
                while (SDL.SDL_PollEvent(out var e) != 0)
                {
                    OnEvent(e);
                }

                Update();
                
                SDL.SDL_RenderClear(Renderer.GetPtr());
                Draw(Renderer.GetPtr());
                SDL.SDL_RenderPresent(Renderer.GetPtr());
                SDL.SDL_SetRenderDrawColor(Renderer.GetPtr(), 0, 0, 0, 255);
            }
        }

        protected virtual void OnEvent(SDL.SDL_Event ev)
        {
            switch (ev.type) 
            {
                // program exit
                case (SDL.SDL_EventType.SDL_QUIT):
                    quit = true;
                    break;
                
                // TODO method based input:
                case (SDL.SDL_EventType.SDL_KEYUP):
                    break;
                
                case (SDL.SDL_EventType.SDL_KEYDOWN):
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONUP):
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN):
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEWHEEL):
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEMOTION):
                    break;
            }
        }

        protected virtual void Load()
        {
        }

        protected virtual void Update()
        {
            ScreenMgr.Update();
        }

        // it's probably more efficient to pass around pointers than actual objects
        // can change this later though if need be
        protected virtual void Draw(IntPtr ren)
        {
            ScreenMgr.Draw(ren);
        }

        public void Dispose()
        {
            ScreenMgr.Dispose();
            Content.Dispose();
            SDL.SDL_Quit();
        }
    }
}