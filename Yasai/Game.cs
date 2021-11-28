using System;
using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
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

        readonly float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        private Shader shader;

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
            
            GL.ClearColor(Color.CornflowerBlue);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            shader = new Shader(@"Assets/shader.vert", @"Assets/shader.frag");
        }

        public override void Unload(DependencyContainer dependencies)
        {
            base.Unload(dependencies);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vbo);
            shader.Dispose();
        }

        public sealed override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            Window.SwapBuffers();
        }
    }
}