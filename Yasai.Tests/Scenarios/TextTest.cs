using OpenTK.Mathematics;
using Yasai.Graphics.Text;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class TextTest : Scenario
    {
        private SpriteText s;
        public TextTest()
        {
            Add(s = new SpriteText("big one 何", "tahoma")
            {
                Position = new Vector2(400),
            });
        }

        public override void Load(ContentStore cs)
        {
            cs.LoadResource("tahoma.ttf");
            base.Load(cs);
        }
    }
}