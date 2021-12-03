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
            Window.RenderFrame += Draw;
            Window.Resize += Resize;

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
            container.Scale = Window.Size / 2;
        }

        protected int VertexArrayObject;

        private Box box, box2, box3;

        private Sprite spr;

        private WangContainer container;

        private TextureStore texStore;
        
        public virtual void Load(DependencyContainer dependencies)
        { 
            GL.ClearColor(Color.CornflowerBlue);
            
            // VertexArrayObject
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            container = new WangContainer
            {
                Scale = Window.Size / 2, // <- wot
                Colour = Color.Green,
                Fill = true,
                Items = new IDrawable[]
                {
                    new Box
                    {
                        Position = Vector2.Zero,
                        Scale = new Vector2(0.5f),
                        Colour = Color.Red,
                        Anchor = Anchor.Center,
                        Origin = Anchor.Center,
                    }
                }
            };
            container.Load(dependencies);
            
            box2 = new Box
            {
                Position = new Vector2(0),
                Scale = new Vector2(200),
                Colour = Color.FromArgb(255,69,255,78)
            };
            box2.Load(dependencies);
            
            box = new Box
            {
                Position = new Vector2(300),
                Scale = new Vector2(40),
                Colour = Color.FromArgb(255,255,255,78)
            };
            box.Load(dependencies);
            
            box3 = new Box
            {
                Position = new Vector2(300,400),
                Scale = new Vector2(40),
                Colour = Color.FromArgb(255,23,140,170),
                Alpha = 0.5f
            };
            box3.Load(dependencies);

            texStore = new TextureStore(dependencies);
            texStore.LoadResource(@"tex.png");
            spr = new Sprite(texStore.GetResource("tex"))
            {
                Position = new Vector2(300),
                Scale = new Vector2(80),
                Origin = Anchor.TopLeft,
                Colour = Color.Aqua,
            };
            spr.Load(dependencies);
        }

        public virtual void Update(FrameEventArgs args)
        {
            container.Update(args);
        }

        private float time;

        public virtual void Draw(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(VertexArrayObject);

            time += (float)args.Time;

           //container.X += 1f;
           //container.Y--;
           //container.Height++;
           //container.Rotation += 0.01f;
            container.Draw();
           //container.Rotation += 0.05f;
           //container.Height += 0.5f;
           //DrawPrimitive(box);
           //DrawPrimitive(spr);
           //DrawPrimitive(box3);

            //spr.Rotation = (time) % (2 * (float)Math.PI);
            //spr.Scale = new Vector2(time*40);
            box3.Y = time*80;
            
            Window.SwapBuffers();
        }

        public virtual void Unload(DependencyContainer dependencies)
        {
            box.Dispose();
            box2.Dispose();
            spr.Dispose();
            box3.Dispose();
            
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
