using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Resources;

namespace Yasai.VisualTests.Scenarios
{
    [TestScenario]
    public class ResourceTest : Scenario
    {
        private ContentStore store;
        
        public ResourceTest(Game game) 
            : base (game)
        {
            store = new ContentStore(Game);
        }

        public override void LoadComplete()
        {
            base.LoadComplete();
            Add(new SpriteText ("WIP test currently not all cache functionality is being implemented right now", "fnt_smallFont")
            {
                Position = new Vector2(200)
            });
        }
    }
}