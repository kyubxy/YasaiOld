using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Loaders;

namespace Yasai.VisualTests.Scenarios
{
    [TestScenario]
    public class TextTest : Scenario
    {
        private SpriteText s;
        public TextTest(Game game) : base (game)
        {
            Add(s = new SpriteText("1234567890", "tahoma")
            {
                Position = new Vector2(400),
            });
        }

        public override void Load(ContentStore store)
        {
            store.LoadResource("tahoma.ttf", args: new FontArgs(90));
            base.Load(store);
        }

        private int i;
        public override void Update()
        {
            base.Update();
            i++;
            s.Text = i.ToString();
        }
    }
}