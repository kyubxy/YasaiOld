using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;

namespace Yasai.VisualTests.Scenarios.Drawables
{
    [TestScenario]
    public class DrawableBindablePropertiesScenario : Scenario
    {
        public DrawableBindablePropertiesScenario()
        {
            AddAll(new IDrawable[]
            {
                new Group(new IDrawable[]
                {
                    new Box()
                    {
                        Position = new Vector2(20),
                    },
                    new Box()
                    {
                        Position = new Vector2(200),
                    },
                })
                {
                    Colour = Color.Aqua,
                    Fill = true,
                },
                
                new Box()
                {
                    Position = new Vector2(400),
                    Alpha = 0.2f
                }
            });
        }
    }
}