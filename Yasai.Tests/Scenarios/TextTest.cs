using OpenTK.Mathematics;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Loaders;

namespace Yasai.Tests.Scenarios
{
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

        public override void Load(ContentStore cs)
        {
            cs.LoadResource("tahoma.ttf", args: new FontArgs(20));
            base.Load(cs);
        }

        private int i = 0;
        public override void Update()
        {
            i++;
            s.Text = i.ToString();
            
            base.Update();
        }
    }
}