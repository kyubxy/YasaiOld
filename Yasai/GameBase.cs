using System;
using System.Drawing;
using ManagedBass;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Debug.Logging;
using Yasai.Graphics.Text;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// The bare essentials of a Yasai application
    /// </summary>
    public class GameBase : IDisposable
    {
        public readonly GameWindow Window;
        
        // make the field static for now, we'll worry about thread safety later
        public static Matrix4 Projection;

        public DependencyContainer Dependencies { get; }
        
        protected IYasaiArgs YasaiArgs;

        internal static readonly Logger YasaiLogger = new ("yasai.log");

        private Color col;
        public Color BackgroundColor
        {
            get => col;
            set
            {
                col = value;
                GL.ClearColor(col);
            }
        }
        
        public GameBase(string title, GameWindowSettings gameSettings, NativeWindowSettings nativeSettings, string[] args = null)
        {
            // TODO: actually allow modifying of arguments
            YasaiArgs = new YasaiArgs()
            {
                EnableAudio = true,
                EnableInput = true
            };

            // Window
            YasaiLogger.LogInfo("opening window");
            Window = new GameWindow(gameSettings, nativeSettings);
            Window.Title = title;

            YasaiLogger.LogInfo("-- Hardware --");
            YasaiLogger.LogInfo($"Version:    {GL.GetString(StringName.Version)}");
            YasaiLogger.LogInfo($"Vendor:     {GL.GetString(StringName.Vendor)}");
            YasaiLogger.LogInfo($"Renderer:   {GL.GetString(StringName.Renderer)}");
            YasaiLogger.LogInfo($"SL Version: {GL.GetString(StringName.ShadingLanguageVersion)}");
            YasaiLogger.LogInfo($"Extensions: {GL.GetString(StringName.Extensions)}");
            YasaiLogger.LogInfo("----");

            // events
            Window.Load += () => Load(Dependencies);
            Window.Unload += () => Unload(Dependencies);
            Window.UpdateFrame += Update;
            Window.RenderFrame += draw;
            Window.Resize += Resize;
            
            // TODO: need a way to specify which input systems can run
            // flags are preferable
            if (YasaiArgs.EnableInput)
            {
                Window.KeyDown += KeyDown;
                Window.KeyUp += KeyUp;
                Window.MouseMove += MouseMove;
                Window.MouseDown += MouseDown;
                Window.MouseUp += MouseUp;
                Window.MouseWheel += MouseWheel;
            }

            // tests
            YasaiLogger.LogInfo("enabling opengl tests...");
            GL.Enable(EnableCap.Blend);
            
            // audio
            if (YasaiArgs.EnableAudio)
            {
                YasaiLogger.LogInfo("initialising audio engine...");
                Bass.Init();
            }
            
            // Initialise dependencies
            YasaiLogger.LogInfo("setting up dependencies...");
            Dependencies = new DependencyContainer();
            Dependencies.Register<GameWindow>(Window);
            Projection = Matrix4.CreateOrthographicOffCenter(0, Window.Size.X, Window.Size.Y, 0, -1, 1);
            
            YasaiLogger.LogInfo("Splash! (ﾉ´ヮ`)ﾉ*: ･ﾟ ");
        }

        public virtual void Resize(ResizeEventArgs args)
        {
            GL.Viewport(0,0,args.Width, args.Height);
            Projection = Matrix4.CreateOrthographicOffCenter(0, args.Width, args.Height, 0, -1, 1);
            YasaiLogger.LogDebug($"Window resized to: {(args.Width, args.Height)}");
        }

        protected int VertexArrayObject;
        
        public virtual void Load(DependencyContainer dependencies)
        { 
            // VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            
            BackgroundColor = Color.CornflowerBlue;
        }

        public virtual void Update(FrameEventArgs args)
        {
        }

        void draw(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            GL.BindVertexArray(VertexArrayObject);

            Draw(args);
            
            Window.SwapBuffers();
        }
        
        protected virtual void Draw(FrameEventArgs args)
        { }

        public virtual void Unload(DependencyContainer dependencies)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.DeleteVertexArray(VertexArrayObject);
        }

        public void Run() => Window.Run();
        
        #region input
        protected virtual void MouseWheel(MouseWheelEventArgs args)
        { }

        protected virtual void MouseUp(MouseButtonEventArgs args)
        { }

        protected virtual void MouseDown(MouseButtonEventArgs args)
        { }

        protected virtual void MouseMove(MouseMoveEventArgs args)
        { }

        protected virtual void KeyUp(KeyboardKeyEventArgs args)
        { }

        protected virtual void KeyDown(KeyboardKeyEventArgs args)
        { }
        #endregion
        
        public void Dispose()
        {
            // TODO: dispose disposable dependencies
            if (YasaiArgs.EnableAudio)
                Bass.Free();
            YasaiLogger.LogInfo("Disposed of resources and exited successfully");
        }
    }
}
