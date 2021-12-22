using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private readonly Box box;

        private SpriteText text;

        public TestGame()
        {
            FontStore fontStore = new FontStore(Dependencies);
            fontStore.LoadResource(@"Fonts\OpenSans-Regular.ttf", "normal", new FontArgs(32));
            
            Children = new IDrawable[]
            {
                box = new Box()
                {
                    Anchor = Anchor.Center,
                    Origin = Anchor.Center,
                    Size = new Vector2(200),
                    Colour = Color.Aqua
                },
                text = new SpriteText("wangs", fontStore.GetResource("normal"))
                {
                    
                }
            };
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            box.Rotation += 0.01f;
        }
    }
}