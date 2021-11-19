using System.Reflection;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        protected Group Root;

        private FontStore fontStore;

        
        #region constructors
        public Game(string[] args = null) 
            : this(60, args) 
        { }
        
        public Game (int refreshRate, string[] args = null) 
            : this ($"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name} @ {refreshRate}Hz", refreshRate, args) 
        { }
        
        public Game (string title, int refreshRate = 60, string[] args = null) 
            : this (title, 1366, 768, refreshRate, args)
        { }
        
        public Game(string title, int w, int h, int refreshRate, string[] args = null) 
            : base (title, w, h, refreshRate, args)
        {
            Root = new Group();
            Children.Add(Root);

            // register font store 
            Dependencies.Register<FontStore>(fontStore = new FontStore(Dependencies, @"Assets/Fonts"));
        }
        #endregion

        /// <summary>
        /// Load framework related resources
        /// </summary>
        private void yasaiLoad()
        {
            // fonts
            fontStore.LoadResource(@"OpenSans-Regular.ttf", SpriteFont.FontTiny, new FontArgs(14));
            fontStore.LoadResource(@"LigatureSymbols.ttf", SpriteFont.SymbolFontTiny, new FontArgs(14));
        }

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
        }
    }
}