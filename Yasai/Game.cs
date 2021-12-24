using System;
using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shaders;
using Yasai.Graphics.Shapes;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        protected Container Root;

        // you can only set this before Load()
        protected IDrawable[] Children = Array.Empty<IDrawable>();

        //private FontStore fontStore;
        private ShaderStore shaderStore;

        private static readonly string DEFAULT_NAME = $"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name}";

        #region constructors
        
        public Game(string title=null, int w=1366, int h=768, string[] args = null) 
            : this (title ?? DEFAULT_NAME, GameWindowSettings.Default, new NativeWindowSettings { Size = new Vector2i(w, h) }, args)
        { }
        
        public Game(string title, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string[] args = null) 
            : base (title, gameWindowSettings, nativeWindowSettings, args)
        {
            Root = new Container()
            {
                Size = nativeWindowSettings.Size
            };

            // register font store 
            //Dependencies.Register<FontStore>(fontStore = new FontStore(Dependencies, @"Assets/Fonts"));
            Dependencies.Register<ShaderStore>(shaderStore = new ShaderStore(Dependencies, @"Assets/Shaders"));
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
           
            shaderStore.LoadResource("texture.sh", Shader.TextureShader);
            shaderStore.LoadResource("solid.sh", Shader.SolidShader);
        }
        
        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
            Root.AddAll(Children);
            Root.Load(dependencies);
        }

        public override void Resize(ResizeEventArgs obj)
        {
            base.Resize(obj);
            Root.Size = Window.Size;
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            Root.Update(args);
        }

        protected override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            Root.Draw();
        }
    }
}