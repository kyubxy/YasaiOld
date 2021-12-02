using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Debug.Logging;
using Yasai.Structures.DI;
using Yasai.Graphics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shaders;
using Yasai.Graphics.Shapes;

namespace Yasai
{
    /// <summary>
    /// The bare essentials of a Yasai application
    /// </summary>
    public class GameBase : IDisposable
    {
        public readonly GameWindow Window;
        
        // encapsulate the projection matrix for now
        protected Matrix4 Projection;

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
            Window.RenderFrame += Draw;
            Window.Resize += Resize;
            
            // Initialise dependencies
            Dependencies = new DependencyContainer();
            Dependencies.Register<GameWindow>(Window);

            //Children = new Container();
        }

        public virtual void Resize(ResizeEventArgs obj)
        {
            GL.Viewport(0,0,obj.Width, obj.Height);
            Projection = Matrix4.CreateOrthographicOffCenter(0, Window.Size.X, Window.Size.Y, 0, -1, 1); 
        }

        protected int VertexArrayObject;

        private Box box;
        
        public virtual void Load(DependencyContainer dependencies)
        { 
            GL.ClearColor(Color.CornflowerBlue);
            
            // VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            
            box = new Box
            {
                Position = new Vector2(300),
                Size = new Vector2(100),
                Colour = Color.FromArgb(255,255,255,78)
            };
            box.Load(dependencies);
        }

        public virtual void Update(FrameEventArgs args)
        { }

        private float time;

        public virtual void Draw(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(VertexArrayObject);

            time += (float)args.Time * 20;
            
            DrawPrimitive(box);
            
            Window.SwapBuffers();
        }

        /// <summary>
        /// Render a single <see cref="Primitive"/> to the screen
        /// </summary>
        /// <param name="primitive"></param>
        private void DrawPrimitive(Primitive primitive)
        {
            var shader = primitive.Shader;
            
            shader.Use();
            primitive.Use();
            
            // assuming the drawable uses a vertex shader with model and projection matrices
            shader.SetMatrix4("model", primitive.ModelTransforms);
            shader.SetMatrix4("projection", Projection);
            
            GL.DrawElements(PrimitiveType.Triangles, primitive.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public virtual void Unload(DependencyContainer dependencies)
        {
            box.Dispose();
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.DeleteVertexArray(VertexArrayObject);
        }

        public void Run() => Window.Run();
        
        public void Dispose()
        {
            // TODO: dispose disposable dependencies
            YasaiLogger.LogInfo("Disposed of resources and exited successfully");
        }
    }
}
