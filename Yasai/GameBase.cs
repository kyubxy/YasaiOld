using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Debug.Logging;
using Yasai.Structures.DI;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shapes;
using Yasai.Resources.Stores;

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
        
        //protected Container Children;

        internal static readonly Logger YasaiLogger = new ("yasai.log");

        public GameBase(string title, GameWindowSettings gameSettings, NativeWindowSettings nativeSettings, string[] args = null)
        {
            // Window
            Window = new GameWindow(gameSettings, nativeSettings);
            Window.Title = title;

            Window.Load += () => Load(Dependencies);
            Window.Unload += () => Unload(Dependencies);
            Window.UpdateFrame += Update;
            Window.RenderFrame += draw;
            Window.Resize += Resize;
            //Window.KeyDown += KeyDown;
            //Window.KeyUp += KeyUp;
            //Window.MouseMove += MouseMove;
            //Window.MouseDown += MouseDown;
            //Window.MouseUp += MouseUp;
            //Window.MouseWheel += MouseWheel;

            GL.Enable(EnableCap.Blend);
            
            // Initialise dependencies
            Dependencies = new DependencyContainer();
            Dependencies.Register<GameWindow>(Window);
            Projection = Matrix4.CreateOrthographicOffCenter(0, Window.Size.X, Window.Size.Y, 0, -1, 1); 

            //Children = new Container();
        }

        public virtual void Resize(ResizeEventArgs obj)
        {
            GL.Viewport(0,0,obj.Width, obj.Height);
            Projection = Matrix4.CreateOrthographicOffCenter(0, Window.Size.X, Window.Size.Y, 0, -1, 1);
        }

        protected int VertexArrayObject;
        
        public virtual void Load(DependencyContainer dependencies)
        { 
            GL.ClearColor(Color.CornflowerBlue);
            
            // VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
        }

        public virtual void Update(FrameEventArgs args)
        {
        }

        void draw(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
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
        private void MouseWheel(MouseWheelEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void MouseUp(MouseButtonEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void MouseDown(MouseButtonEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void MouseMove(MouseMoveEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void KeyUp(KeyboardKeyEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void KeyDown(KeyboardKeyEventArgs obj)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        public void Dispose()
        {
            // TODO: dispose disposable dependencies
            YasaiLogger.LogInfo("Disposed of resources and exited successfully");
        }
    }
}
