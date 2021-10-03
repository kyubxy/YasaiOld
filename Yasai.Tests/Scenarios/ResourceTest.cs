using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class ResourceTest : Scenario
    {
        private ContentCache cache;
        
        public ResourceTest(Game game) 
            : base (game)
        {
            cache = new ContentCache(Game);
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            Add(new SpriteText ("WIP test currently not all cache functionality is being implemented right now", "fnt_smallFont")
            {
                Position = new Vector2(200)
            });
        }
    }
}