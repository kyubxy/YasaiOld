using OpenTK.Mathematics;
using Yasai.Graphics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Input.Mouse;

namespace Yasai.Tests.Scenarios
{
    public class PrimitivesTest : Scenario
    {
        private Line line;
        private Box box;
        
        public PrimitivesTest()
        {
            AddAll(new IDrawable[]
            {
                new PrimitiveBox()
                {
                    Position = new Vector2(300),
                    Size = new Vector2(300),
                    Colour = Color4.Tomato,
                },
                line = new Line()
                {
                    Position = new Vector2(700,400),
                    StartPosition = Vector2.Zero,
                    EndPosition = new Vector2(200),
                    Colour = Color4.LawnGreen
                },
                box = new Box ()
                {
                    Position = (300,600),
                    Size = new Vector2(200),
                    Alpha = 0.5f,
                    Colour = Color4.Orchid
                }
            });
        }

        public override void MouseDown(MouseArgs args)
        {
            base.MouseDown(args);
            line.EndPosition = Vector2.Subtract(line.EndPosition, new Vector2(0f, 10f));
        }

        public override void Update()
        {
            base.Update();
            box.Rotation += 0.01f;
        }
    }
}