using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;

namespace Yasai.VisualTests.Scenarios.Drawables
{
    [TestScenario]
    public class DrawableSceneGraphScenario : Scenario
    {
        public DrawableSceneGraphScenario()
        {
            AddAll(new IDrawable[]
            {
                new Box
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Size = new (80),
                    Colour = Color.MediumSeaGreen
                },
                // test translation
                new Container
                {
                    Position = new Vector2(100),
                    Items = new IDrawable[]
                    {
                        new Box
                        {
                            Size = new (80),
                            Colour = Color.SlateBlue
                        },
                    }
                },
                // test rotation
                new Container
                {
                    Position = new Vector2(100, 200),
                    Rotation = (float)Math.PI/4,
                    Items = new IDrawable[]
                    {
                        new Box
                        {
                            Colour = Color.DarkOrchid
                        },
                    }
                },
                // test scale
                new Container 
                {
                    Position = new Vector2(200,100),
                    Scale = new Vector2(0.5f, 2),
                    Items = new IDrawable[]
                    {
                        new Box
                        {
                            Colour = Color.Fuchsia,
                            Size = new Vector2(70)
                        },
                    }
                },
                // test nesting
                new Container(new IDrawable[]
                {
                    new Container(new IDrawable[]
                    {
                        new Container(new IDrawable[]
                        {
                            new Box
                            {
                                Colour = Color.Maroon
                            },
                        })
                        {
                            Position = new Vector2(100),
                            Rotation = 0.1f,
                            Colour = Color.Aqua,
                        }
                    })
                    {
                        Position = new Vector2(100),
                        Rotation = 0.1f,
                        Colour = Color.Blue,
                        Size = new Vector2(80),
                        Fill = true
                    }
                })
                {
                    Position = new Vector2(100, 400),
                    Rotation = 0.1f,
                    Colour = Color.Coral,
                    Fill = true
                }
            });
        }
    }
}