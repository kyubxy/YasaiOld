using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Primitives;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class DrawableVisibleEnableTest : Scenario
    {
        public override void Start(ContentCache cache)
        {
            base.Start(cache);
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
    }
}