using System;
using OpenTK.Mathematics;
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
    public class Game : IDisposable, IKeyListener, IMouseListener
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; } 
        
        private bool quit; 
        
        private string title = "Yasai";

        protected ScreenManager ScreenMgr;
        protected Screen Children;
        protected ContentCache Content;

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
            Content = new ContentCache(this);
           
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
                    KeyUp(ev.key.keysym.sym.ToYasaiKeyCode());
                    break;
                
                case (SDL.SDL_EventType.SDL_KEYDOWN):
                    KeyDown(ev.key.keysym.sym.ToYasaiKeyCode());
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONUP):
                    MouseUp(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN):
                    MouseDown(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEWHEEL):
                    // TODO: mousewheel
                    break;
                
                case (SDL.SDL_EventType.SDL_MOUSEMOTION):
                    MouseMotion(new MouseArgs(new Vector2(ev.button.x, ev.button.y)));
                    
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

        // bruh
        public bool IgnoreHierachy => true;
        
        public virtual void MouseDown(MouseArgs args)
        {
            ScreenMgr.MouseDown(args);
            int iv = 0;
            foreach (var k in ScreenMgr.CurrentScreen)
            {
                IMouseListener listener = k as IMouseListener;
            
                if (listener == null) 
                    continue;
            
                if (iv == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy) 
                    listener.MouseDown(args);
            
                iv++;
            }
        }

        public virtual void MouseUp(MouseArgs args)
        {
            ScreenMgr.MouseUp(args);
            int iii = 0;
            foreach (var k in ScreenMgr.CurrentScreen)
            {
                IMouseListener listener = k as IMouseListener;
            
                if (listener == null) 
                    continue;
            
                if (iii == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                    listener.MouseUp(args);
            
                iii++;
            }
        }

        public virtual void MouseMotion(MouseArgs args)
        {
            ScreenMgr.MouseMotion(args);
            int v = 0;
            foreach (var k in ScreenMgr.CurrentScreen)
            {
                IMouseListener listener = k as IMouseListener;
            
                if (listener == null) 
                    continue;
            
                if (v == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                    listener.MouseMotion(args);
            
                v++;
            }
        }

        public virtual void KeyUp(KeyCode key)
        {
            ScreenMgr.KeyUp(key);
            int i = 0;
            foreach (var k in ScreenMgr.CurrentScreen)
            {
                IKeyListener listener = k as IKeyListener;
            
                if (listener == null) 
                    continue;
            
                if (i == ScreenMgr.CurrentScreen.Count - 1  || listener.IgnoreHierachy)
                    listener.KeyUp(key);
            
                i++;
            }
        }

        public virtual void KeyDown(KeyCode key)
        {
            ScreenMgr.KeyDown(key);
            
            int ii = 0;
            foreach (var k in ScreenMgr.CurrentScreen)
            {
                IKeyListener listener = k as IKeyListener;
            
                if (listener == null) 
                    continue;
            
                if (ii == ScreenMgr.CurrentScreen.Count - 1 || listener.IgnoreHierachy)
                    listener.KeyDown(key);
            
                ii++;
            }
        }
    }
}