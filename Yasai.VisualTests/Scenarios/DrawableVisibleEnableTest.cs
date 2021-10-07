using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Primitives;
using Yasai.Resources;

namespace Yasai.VisualTests.Scenarios
{
    [TestScenario]
    public class DrawableVisibleEnableTest : Scenario
    {
        public override void LoadComplete()
        {
            base.LoadComplete();
            AddAll(new IDrawable[]
            {
                new Box()
                {
                    Position = new Vector2(300),
                    Size = new Vector2(100),
                    Colour = Color.Tomato
                }
            });
        }

        public DrawableVisibleEnableTest(Game game) : base(game)
        {
        }
    }
}