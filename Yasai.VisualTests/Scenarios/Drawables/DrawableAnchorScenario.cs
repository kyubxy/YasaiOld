using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Primitives;

namespace Yasai.VisualTests.Scenarios.Drawables
{
    [TestScenario]
    public class DrawableAnchorScenario : Scenario
    {
        public DrawableAnchorScenario()
        {
            AddAll(new IDrawable[]
            {
                new Box
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft, 
                },
                new Box
                {
                    Anchor = Anchor.Top,
                    Origin = Anchor.Top
                },
                new Box
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight
                },
                new Box
                {
                    Anchor = Anchor.Left,
                    Origin = Anchor.Left
                },
                new Box
                {
                    Anchor = Anchor.Center,
                    Origin = Anchor.Center
                },
                new Box
                {
                    Anchor = Anchor.Right,
                    Origin = Anchor.Right
                },
                new Box
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft
                },
                new Box
                {
                    Anchor = Anchor.Bottom,
                    Origin = Anchor.Bottom
                },
                new Box
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight
                },
            });
        }
    }
}