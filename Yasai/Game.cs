using System.Reflection;
using Yasai.Graphics.Groups;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        protected Group Root;
        
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
            Children.Add(FrameRateCounter);

            // resource dependencies
           //Dependencies = new DependencyContainer();
           //fontStore = new ContentStore(this);
           //Dependencies.Register<ContentStore>("fonts");
           //Dependencies.Register<Game>(this);
        }
        #endregion

        /// <summary>
        /// Load framework related resources
        /// </summary>
        private void yasaiLoad()
        {
            //store.LoadResource("Yasai/OpenSans-Regular.ttf", SpriteFont.TinyFont, new FontArgs(12));
            //fontStore.LoadResource("Yasai/OpenSans-Regular.ttf", SpriteFont.TinyFont, new FontArgs(12));
        }

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
        }
    }
}