using System.Numerics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class KeyboardTest : Scenario
    {
        public KeyboardTest()
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

        public override void Load(ContentCache cache)
        {
            cache.LoadResource("tahoma.ttf");
            base.Load(cache);
        }

        sealed class KeyReceiver : Group
        {
            private PrimitiveBox _primitiveBox;

            private SpriteText display;
            private string text;

            public KeyReceiver(bool ignoreHierachy, string text)
            {
                IgnoreHierarchy = ignoreHierachy;
                this.text = text;
            }

            public override void Load(ContentCache cache)
            {
                Add(_primitiveBox = new PrimitiveBox()
                {
                    Position = Position,
                    Size = new Vector2(100),
                    Fill = false
                });

                Add(display = new SpriteText(text, "tahoma")
                {
                    Position = Vector2.Add(Position, new Vector2(200, 0))
                });

                base.Load(cache);
            }

            public override void KeyUp(KeyCode key)
            {
                base.KeyUp(key);
                _primitiveBox.Fill = false;
            }

            public override void KeyDown(KeyCode key)
            {
                base.KeyDown(key);
                _primitiveBox.Fill = true;
                display.Text = key.ToString();
            }
        }
    }
}