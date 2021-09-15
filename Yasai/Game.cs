using System;
using SDL2;
using Yasai.Extensions;
using Yasai.Graphics.Layout;
using Yasai.Graphics.YasaiSDL;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
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
                
                #region input systems
                case (SDL.SDL_EventType.SDL_KEYUP):
                    int i = 0;
                    foreach (var k in ScreenMgr.CurrentScreen)
                    {
                        IKeyListener listener = k as IKeyListener;
                    
                        if (listener == null) 
                            continue;
                    
                        if (i == ScreenMgr.CurrentScreen.Count - 1  || listener.IgnoreHierachy)
                            listener.KeyUp(ev.key.keysym.sym.ToYasaiKeyCode());
                    
                        i++;
                    }
                    break;
                
                case (SDL.SDL_EventType.SDL_KEYDOWN):
                    int ii = 0;
                    foreach (var k in ScreenMgr.CurrentScreen)
                    {
                        IKeyListener listener = k as IKeyListener;

                        if (listener == null) 
                            continue;

                        if (ii == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                            listener.KeyDown(ev.key.keysym.sym.ToYasaiKeyCode());

                        ii++;
                    }
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONUP):
                   int iii = 0;
                   foreach (var k in ScreenMgr.CurrentScreen)
                   {
                       IMouseListener listener = k as IMouseListener;
                
                       if (listener == null) 
                           continue;
                
                       if (iii == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                           listener.MouseUp((MouseButton)ev.button.button);
                
                       iii++;
                   }
                   break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN):
                    int iv = 0;
                    foreach (var k in ScreenMgr.CurrentScreen)
                    {
                        IMouseListener listener = k as IMouseListener;
                    
                        if (listener == null) 
                            continue;
                    
                        if (iv == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                            listener.MouseDown((MouseButton)ev.button.button);
                    
                        iv++;
                    }
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEWHEEL):
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEMOTION):
                    break;
                
                #endregion
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