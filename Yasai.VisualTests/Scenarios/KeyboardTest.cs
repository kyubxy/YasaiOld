using System;
using System.Numerics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;

namespace Yasai.VisualTests.Scenarios
{
    [TestScenario]
    public class KeyboardTest : Scenario
    {
        public KeyboardTest(Game g) : base (g)
        {
            Add(new KeyReceiver(false, "lowest one (not focused)")
            {
                Position = new Vector2(300, 100)
            });

            // sigma ir
            Add(new KeyReceiver(true, "second one (not focused but ignoring hierarchy )")
            {
                Position = new Vector2(300)
            });

            Add(new KeyReceiver(false, "front one (focused)")
            {
                Position = new Vector2(300, 500)
            });
        }

        public override void Load(ContentStore store)
        {
            store.LoadResource("tahoma.ttf");
            base.Load(store);
        }

        sealed class KeyReceiver : Group
        {
            private PrimitiveBox primitiveBox;

            private SpriteText display;
            private string text;

            public KeyReceiver(bool ignoreHierachy, string text)
            {
                IgnoreHierarchy = ignoreHierachy;
                this.text = text;
            }

            public override void Load(ContentStore store)
            {
                Add(primitiveBox = new PrimitiveBox()
                {
                    Position = Position,
                    Size = new Vector2(100),
                    Fill = false
                });

                Add(display = new SpriteText(text, "tahoma")
                {
                    Position = Vector2.Add(Position, new Vector2(200, 0))
                });

                base.Load(store);
            }

            public override void KeyUp(KeyArgs key)
            {
                base.KeyUp(key);
                primitiveBox.Fill = false;
                Console.WriteLine(key);
            }

            public override void KeyDown(KeyArgs key)
            {
                base.KeyDown(key);
                primitiveBox.Fill = true;
                display.Text = key.ToString();
            }
        }
    }
}