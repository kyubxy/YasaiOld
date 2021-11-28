using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Mouse;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.Scenarios.Input
{
    [TestScenario]
    public class MouseInputScenario : Scenario
    {
        sealed class BasicMouseConsumer : Box
        {
            public BasicMouseConsumer()
            {
                Colour = Color.Yellow;
            }

            public override void MouseDown(MouseArgs args)
            {
                base.MouseDown(args);
                switch (args.Button)
                {
                    case MouseButton.Left:
                        Colour = Color.Red;
                        break;
                    case MouseButton.Right:
                        Colour = Color.MediumVioletRed;
                        break;
                    case MouseButton.Middle:
                        Colour = Color.Crimson;
                        break;
                }
            }

            public override void MouseMotion(MouseArgs args)
            {
                base.MouseMotion(args);
                Colour = Color.Green;
            }

            public override void MouseUp(MouseArgs args)
            {
                base.MouseUp(args);
                Colour = Color.Blue;
            }
        }

        sealed class EventMouseConsumer : Container
        {
            SpriteText statusText;

            private int _clicks;
            private int clicks
            {
                get => _clicks;
                set
                {
                    _clicks = value;
                    statusText.Text = _clicks.ToString();
                }
            }

            public override void Load(DependencyContainer dependencies)
            {
                base.Load(dependencies);
                
                Box box1; 
                Box box2;

                var font = dependencies.Resolve<FontStore>().GetResource(SpriteFont.FontTiny);
                
                Add(box1 = new Box()
                {
                    Position = Position,
                    Colour = Color.CornflowerBlue,
                    Size = Size
                });
                Add(box2 = new Box()
                {
                    Position = Vector2.Add(Position, new Vector2(0,Size.Y + 10)),
                    Colour = Color.Orchid,
                    Size = new Vector2(20)
                });
                Add(statusText = new SpriteText("0", font)
                {
                    Position = new Vector2(X, box1.Y + box1.Size.Y ),
                    Colour = Color.White
                });

                box1.OnClick += (_, _)   => clicks++;
                box1.OnRelease += (_, _) => clicks--;
                box1.OnEnter += (_, _)   => box1.Colour = Color.Beige;
                box1.OnExit += (_, _)    => box1.Colour = Color.Aqua;
                box1.OnHover += (_, args) => box2.Position = ((MouseArgs)args).Position;
            }
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);

            var fontStore = dependencies.Resolve<FontStore>();
            var font = fontStore.GetResource(SpriteFont.FontTiny);

            AddAll(new IDrawable[]
            {
                new BasicMouseConsumer
                {
                    Position = new Vector2(50, 200),
                    Size = new Vector2(100)
                },
                new SpriteText("basic functionality", font)
                {
                    Position = new Vector2(50, 180)
                },
                new EventMouseConsumer
                {
                    Position = new Vector2(230, 200),
                    Size = new Vector2(100)
                },
                new SpriteText("events", font)
                {
                    Position = new Vector2(230, 180)
                },
                new Container(new IDrawable[]
                {
                    new EventMouseConsumer
                    {
                        Position = new Vector2(400, 200),
                        Size = new Vector2(100),
                    },
                    new EventMouseConsumer
                    {
                        Position = new Vector2(450, 250),
                        Size = new Vector2(100),
                    }
                }),
                new SpriteText("stacked", font)
                {
                    Position = new Vector2(400, 180)
                },
            });
        }
    }
}