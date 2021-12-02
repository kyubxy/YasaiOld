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
using Yasai.Resources.Stores;

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

            GL.Enable(EnableCap.Blend);
            
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

        private Box box, box2, box3;

        private Sprite spr;

        private TextureStore texStore;
        
        public virtual void Load(DependencyContainer dependencies)
        { 
            GL.ClearColor(Color.CornflowerBlue);
            
            // VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            
            box = new Box
            {
                Position = new Vector2(300),
                Size = new Vector2(40),
                Colour = Color.FromArgb(255,255,255,78)
            };
            box.Load(dependencies);
            
            box2 = new Box
            {
                Position = new Vector2(300, 400),
                Size = new Vector2(40),
                Colour = Color.FromArgb(255,69,255,78)
            };
            box2.Load(dependencies);
            
            box3 = new Box
            {
                Position = new Vector2(300,400),
                Size = new Vector2(40),
                Colour = Color.FromArgb(255,23,140,170),
            };
            box3.Load(dependencies);

            texStore = new TextureStore(dependencies);
            texStore.LoadResource(@"tex.png");
            spr = new Sprite(texStore.GetResource("tex"))
            {
                Position = new Vector2(300),
                Size = new Vector2(80),
                Origin = Anchor.TopLeft,
                Colour = Color.Aqua,
            };
            spr.Load(dependencies);
        }

        public virtual void Update(FrameEventArgs args)
        { }

        private float time;

        public virtual void Draw(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(VertexArrayObject);

            time += (float)args.Time;
            
            DrawPrimitive(box);
            DrawPrimitive(box2);
            DrawPrimitive(box3);
            DrawPrimitive(spr);

            //spr.Rotation = (time) % (2 * (float)Math.PI);
            spr.Size = new Vector2(time*40);
            
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
            box2.Dispose();
            box3.Dispose();
            spr.Dispose();
            
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
