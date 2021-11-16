using System.Reflection;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;
using Yasai.Resources.Loaders;
using Yasai.Structures;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        private ContentCache _content;

        public DependencyCache Dependencies;
        
        private ContentCache fontCache;

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
            Content = new ContentCache(this);
            
            Root = new Group();
            Children.Add(Root);
            Children.Add(FrameRateCounter);

            // resource dependencies
            Dependencies = new DependencyCache();
            fontCache = new ContentCache(this);
            Dependencies.Register<ContentCache>("fonts");
            Dependencies.Register<Game>(this);
        }
        #endregion

        /// <summary>
        /// Load framework related resources
        /// </summary>
        /// <param name="cache"></param>
        private void yasaiLoad(ContentCache cache)
        {
            cache.LoadResource("Yasai/OpenSans-Regular.ttf", SpriteFont.TinyFont, new FontArgs(12));
            fontCache.LoadResource("Yasai/OpenSans-Regular.ttf", SpriteFont.TinyFont, new FontArgs(12));
        }

        public override void Load(ContentCache cache)
        {
            yasaiLoad(cache);
            base.Load(cache);
        }
    }
}