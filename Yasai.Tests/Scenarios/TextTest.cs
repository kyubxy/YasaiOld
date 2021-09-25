using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Loaders;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class TextTest : Scenario
    {
        private SpriteText s;
        public TextTest()
        {
            Add(s = new SpriteText("1234567890", "tahoma")
            {
                Position = new Vector2(400),
            });
        }

        public override void Load(ContentCache cache)
        {
            cache.LoadResource("tahoma.ttf", args: new FontArgs(90));
            base.Load(cache);
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