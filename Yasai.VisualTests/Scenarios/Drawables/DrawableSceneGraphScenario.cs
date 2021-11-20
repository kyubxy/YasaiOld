using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Primitives;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.Scenarios.Drawables
{
    [TestScenario]
    public class DrawableSceneGraphScenario : Scenario
    {
        public DrawableSceneGraphScenario()
        {
        }

        private Texture tex;
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            var store = dependencies.Resolve<TextureStore>();
            tex = store.GetResource("ino");
            
            AddAll(new IDrawable[]
            {
                someOfTest,
            });
        }

        private Container cont;
        private Drawable box;

        public override void Update()
        {
            base.Update();
            cont.Rotation += 0.01f;
            box.X++;
        }

        Container someOfTest => new (new IDrawable[]
        {
            /*
            box = new Sprite(tex)
            {
                Position = new Vector2(200)
            }
            */
            cont = new Container
            {
                Fill = true,
                Position = new Vector2(200),
                Items = new IDrawable[]
                {
                    box = new Box()
                    {
                        Colour = Color.MediumSpringGreen
                    }
                }
            }
        });

        Container restOfTest => new(new IDrawable[]
        {
            new Box
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                Size = new(80),
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
                        Size = new(80),
                        Colour = Color.SlateBlue
                    },
                }
            },
            // test rotation
            new Container
            {
                Position = new Vector2(100, 200),
                Rotation = (float)Math.PI / 4,
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
                Position = new Vector2(200, 100),
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
            new Container
            {
                Rotation = 0.2f,
                Colour = Color.Coral,
                Fill = true,
                Position = new Vector2(100, 400),
                Items = new IDrawable[]
                {
                    new Container
                    {
                        Rotation = 0.2f,
                        Colour = Color.Blue,
                        Fill = true,
                        Items = new IDrawable[]
                        {
                            new Container
                            {
                                Rotation = 0.2f,
                                Colour = Color.Aqua,
                                Fill = true,
                                Items = new IDrawable[]
                                {
                                    new Box
                                    {
                                        Colour = Color.Maroon,
                                        Rotation = 0.2f
                                    },
                                }
                            }
                        }
                    }
                }
            }
        });
    }
}