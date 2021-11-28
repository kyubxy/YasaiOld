using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Debug.Logging;
using Yasai.Structures.DI;
using Yasai.Graphics;

namespace Yasai
{
    /// <summary>
    /// The bare essentials of a Yasai application
    /// </summary>
    public class GameBase : IContainer, IDisposable
    {
        public readonly GameWindow Window;
        
        private bool quit;

        public bool Loaded => true;
        
        public bool Visible { get; set; } = true;
        public void Draw(IntPtr renderer)
        {
            throw new NotImplementedException();
        }

        public bool Enabled
        {
            get => throw new Exception("use Game.Quit() to quit the game");
            set => throw new Exception("use Game.Quit() to quit the game");
        }

        public DependencyContainer Dependencies { get; }
        
        //protected Container Children;

        internal static readonly Logger YasaiLogger = new ("yasai.log");

        #region constructors
        public GameBase(string title, int w, int h, GameWindowSettings gameSettings, NativeWindowSettings nativeSettings, string[] args = null)
        {
            var e = new NativeWindowSettings()
            {
                Size = new Vector2i(800,600)
            };
            
            Window = new GameWindow(gameSettings, e);
            //Window.Size = new Vector2i(800, 600);
            Window.Title = title;

            Window.Load += () => Load(Dependencies);
            Window.Unload += () => Unload(Dependencies);
            Window.UpdateFrame += Update;
            Window.RenderFrame += Draw;
            
            Dependencies = new DependencyContainer();
            Dependencies.Register<GameWindow>(Window);

            //Children = new Container();
        }

        #endregion
        
        public virtual void Load(DependencyContainer dependencies)
        { }

        public virtual void Update(FrameEventArgs args)
        { }
        
        public virtual void Draw(FrameEventArgs args)
        { }

        public virtual void Unload(DependencyContainer dependencies)
        { }

        public void Run() => Window.Run();
        
        public void Dispose()
        {
            // TODO: dispose disposable dependencies
            YasaiLogger.LogInfo("Disposed of resources and exited successfully");
        }
    }
}
