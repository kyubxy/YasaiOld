using System;
using System.Numerics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class MouseTest : Scenario
    {
        public MouseTest()
        {
            Add(new MouseInput(true, true)
            {
                Position = new Vector2(150, 100)
            });

            Add(new MouseInput(false)
            {
                Position = new Vector2(200, 200)
            });

            Add(new MouseInput(false)
            {
                Position = new Vector2(210, 200)
            });

            Add(new MouseInput(false)
            {
                Position = new Vector2(220, 200)
            });
        }

        sealed class MouseInput : Group
        {
            private PrimitiveBox _primitiveBox;
            private bool noisy;

            public MouseInput(bool ignoreHierachy, bool noisy = false)
            {
                IgnoreHierarchy = ignoreHierachy;
                this.noisy = noisy;
            }

            public override void Load(ContentCache cache)
            {
                Add(_primitiveBox = new PrimitiveBox()
                {
                    Position = Position,
                    Size = new Vector2(200),
                    Fill = false
                });

                base.Load(cache);
            }

            public override void MouseDown(MouseArgs args)
            {
                base.MouseDown(args);

                if (args.Button == MouseButton.Left)
                    _primitiveBox.Fill = true;
            }

            public override void MouseUp(MouseArgs args)
            {
                base.MouseUp(args);

                _primitiveBox.Fill = false;
            }
        }
    }
}
