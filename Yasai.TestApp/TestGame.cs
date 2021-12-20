using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private readonly Box box;

        public TestGame()
        {
            Children = new IDrawable[]
            {
                box = new Box()
                {
                    Anchor = Anchor.Center,
                    Origin = Anchor.Center,
                    Size = new Vector2(200),
                    Colour = Color.Aqua
                }
            };
        }

        public override void Update(FrameEventArgs args)
        {
            // still need to fix rotation and origins but that's about as much shit as im willing to take from this
            base.Update(args);
            box.Rotation += 0.01f;
        }
    }
}