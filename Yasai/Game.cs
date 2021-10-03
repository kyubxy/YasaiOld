using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using SDL2;
using Yasai.Debug;
using Yasai.Extensions;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.YasaiSDL;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;
using Yasai.Resources.Loaders;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : IGroup
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; } 
        
        private bool _quit;

        public bool Loaded => Window != null && Renderer != null && _content != null;
        
        public bool Visible { get; set; } = true;
        public bool Enabled
        {
            get => throw new Exception("use Game.Quit() to quit the game");
            set => throw new Exception("use Game.Quit() to quit the game");
        }

        protected Group Children;
        
        private ContentCache _content;

        private DebugOverlay _debugOverlay;
        
        #region constructors
        public Game(string[] args = null) 
            : this(60, args) 
        { }
        
        public Game (int refreshRate, string[] args = null) 
            : this ($"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name} @ {refreshRate}Hz", refreshRate, args) 
        { }
        
        public Game (string title, int refreshRate = 60, string[] args = null) 
            : this (title, 1366, 768, refreshRate, args)
        { }
        
        public Game(string title, int w, int h, int refreshRate, string[] args = null)
        { 
            // SDL initialisation
            if (SDL_Init(SDL_INIT_EVERYTHING) != 0) 
                Console.WriteLine($"error on startup: {SDL_GetError()}");
            TTF_Init();
            
            
            // everything else
            Window = new Window(title, w, h, refreshRate);
            Renderer = new Renderer(Window);           
             
            Children = new Group();
            Children.Add(_debugOverlay = new DebugOverlay());
        }
        #endregion
        
        public void Run()
        {
            _content = new ContentCache(this);

            Start(_content);

            yasaiLoad(_content);
            Children.Load(_content);
            Load(_content);
            
            while (!_quit)
            {
                while (SDL_PollEvent(out var e) != 0)
                    OnEvent(e);

                Update();

                _debugOverlay.FrameRateCounter.StartCount = SDL_GetPerformanceCounter();
                Renderer.Clear();
                Draw(Renderer.GetPtr());
                Renderer.Present();
                Renderer.SetDrawColor(0,0,0,255);
                _debugOverlay.FrameRateCounter.EndCount = SDL_GetPerformanceCounter();
            }
        }

        private HashSet<KeyCode> pressed = new();

        protected virtual void OnEvent(SDL_Event ev)
        {
            switch (ev.type) 
            {
                // program exit
                case (SDL_EventType.SDL_QUIT):
                    _quit = true;
                    break;
                
                #region input systems
                case (SDL_EventType.SDL_KEYUP):
                    pressed.Remove(ev.key.keysym.sym.ToYasaiKeyCode());
                    KeyUp(new KeyArgs(pressed));
                    break;
                
                case (SDL_EventType.SDL_KEYDOWN):
                    pressed.Add(ev.key.keysym.sym.ToYasaiKeyCode());
                    KeyDown(new KeyArgs(pressed));
                    break;
                
                case (SDL_EventType.SDL_MOUSEBUTTONUP):
                    MouseUp(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL_EventType.SDL_MOUSEBUTTONDOWN):
                    MouseDown(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL_EventType.SDL_MOUSEWHEEL):
                    // TODO: mousewheel
                    break;
                
                case (SDL_EventType.SDL_MOUSEMOTION):
                    MouseMotion(new MouseArgs(new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                #endregion
            }
        }

        public virtual void Start(ContentCache cache) => Children.Start(cache);

        /// <summary>
        /// Load framework related resources
        /// </summary>
        /// <param name="cache"></param>
        private void yasaiLoad(ContentCache cache)
        {
            cache.LoadResource("Yasai/OpenSans-Regular.ttf", Constants.tinyFont, new FontArgs(12));
        }
        
        public virtual void Load(ContentCache cache)
        { }

        public virtual void Update() => Children.Update();

        // it's probably more efficient to pass around pointers than actual objects
        // can change this later though if need be
        public virtual void Draw(IntPtr ren)
        {
            if (Visible)
            {
                Children.Draw(ren);
                _debugOverlay.Draw(ren);
            }
            else
            {
#if DEBUG
                Console.WriteLine("Game is invisible. The use of Game.Visible is highly unadvised in the first place!");
#endif
            }
                
        }

        /// <summary>
        /// Quit the current game
        /// </summary>
        public void Quit()
        {
            _quit = false;
            Dispose();
        }

        public void Dispose()
        {
            Children.Dispose();
            _content.Dispose();
            SDL_Quit();
        }

        #region input
        public bool IgnoreHierarchy => true;
        public virtual void MouseDown(MouseArgs args) => Children.MouseDown(args);
        public virtual void MouseUp(MouseArgs args) => Children.MouseUp(args);
        public virtual void MouseMotion(MouseArgs args) => Children.MouseMotion(args);
        public virtual void KeyUp(KeyArgs key) => Children.KeyUp(key);
        public virtual void KeyDown(KeyArgs key) => Children.KeyDown(key);
        #endregion
    }
}