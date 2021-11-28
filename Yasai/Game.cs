using System;
using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
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

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
        }

        public sealed override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CornflowerBlue);
            
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(0,0);
            GL.Vertex2(1,0);
            GL.Vertex2(0,1);
            GL.End();
            
            Window.SwapBuffers();
        }
    }
}