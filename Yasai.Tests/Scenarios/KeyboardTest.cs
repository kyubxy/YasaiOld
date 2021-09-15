using OpenTK.Mathematics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class KeyboardTest : Scenario
    {
        public KeyboardTest()
        {
            Add (new KeyReceiver(false, "lowest one (not focused)")
            {
                Position = new Vector2(300, 100)
            });

            // sigma ir
            Add(new KeyReceiver(true, "second one (not focused but ignoring hierarchy )")
            {
                Position = new Vector2(300)
            });
                
            Add (new KeyReceiver(false, "front one (focused)")
            {
                Position = new Vector2(300, 500)
            });
        }

        public override void Load(ContentStore cs)
        {
            cs.LoadResource("tahoma.ttf");
            base.Load(cs);
        }
    }

    sealed class KeyReceiver : Group, IKeyListener
    {
        public bool IgnoreHierachy { get; }

        private PrimitiveBox _primitiveBox;

        private SpriteText display;
        private string text;

        public KeyReceiver(bool ignoreHierachy, string text)
        {
            IgnoreHierachy = ignoreHierachy;
            this.text = text;
        }

        public override void Load(ContentStore cs)
        {
            Add(_primitiveBox = new PrimitiveBox()
            {
                Position = Position,
                Size = new Vector2(100),
                Fill = false
            });

            Add(display = new SpriteText(text, "tahoma")
            {
                Position = Vector2.Add (Position, new Vector2(200, 0))
            });
            
            base.Load(cs);
        }

        public void KeyUp(KeyCode key)
        {
            _primitiveBox.Fill = false;
        }

        public void KeyDown(KeyCode key)
        {
            _primitiveBox.Fill = true;
            display.Text = key.ToString();
        }
    }
}