using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Graphics;
using Yasai.Graphics.Shaders;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        //protected Container Root;

        //private FontStore fontStore;

        
        #region constructors
        //public Game(string[] args = null) 
        //    : this(60, args) 
        //{ }
        //
        //public Game (int refreshRate, string[] args = null) 
        //    : this ($"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name} @ {refreshRate}Hz", refreshRate, args) 
        //{ }
        //
        //public Game (string title, int refreshRate = 60, string[] args = null) 
        //    : this (title, 1366, 768, refreshRate, args)
        //{ }

        public Game() : this ($"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name}")
        { }
        
        public Game (string title, int w = 1366, int h = 768, string[] args = null) 
            : this (title, w, h, GameWindowSettings.Default, NativeWindowSettings.Default, args)
        { }
        
        public Game(string title, int w, int h, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string[] args = null) 
            : base (title, w, h, gameWindowSettings, nativeWindowSettings, args)
        {
            //Root = new Container();
            //Children.Add(Root);

            // register font store 
            //Dependencies.Register<FontStore>(fontStore = new FontStore(Dependencies, @"Assets/Fonts"));
        }
        #endregion

        /// <summary>
        /// Load framework related resources
        /// </summary>
        private void yasaiLoad()
        {
            // fonts
           //fontStore.LoadResource(@"OpenSans-Regular.ttf", SpriteFont.FontTiny, new FontArgs(14));
           //fontStore.LoadResource(@"LigatureSymbols.ttf", SpriteFont.SymbolFontTiny, new FontArgs(14));
        }

        private int vbo;
        private int vao;

        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 projection;

        private Shader shader;
        private int _elementBufferObject;
        private readonly float[] vertices =
        {
            // Position         TextureTemp coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        private readonly uint[] _indices =
        {
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
            
            GL.ClearColor(Color.CornflowerBlue);
            
            GL.Enable(EnableCap.DepthTest);
            
            // vao
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            // vbo
            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // ebo
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            
            shader = new Shader(@"Assets/shader.vert", @"Assets/shader.frag");
            shader.Use();
            
            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            
            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            tex = new Texture("Assets/tex.png");
            tex.Use();
            
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Window.Size.X / (float) Window.Size.Y, 0.1f, 100.0f);

            // GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0); // <- no images

        }

        private Texture tex;

        public override void Unload(DependencyContainer dependencies)
        {
            base.Unload(dependencies);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
            shader.Dispose();
        }

        private double time;
        public sealed override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            time += args.Time*100;
            
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.BindVertexArray(vao);
            
            tex.Use();
            shader.Use();
            
            model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));
            
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            
            Window.SwapBuffers();
        }
    }
}