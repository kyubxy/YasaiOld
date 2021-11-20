using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.Scenarios.Drawables
{
    [TestScenario]
    public class DrawableRotationScenario : Scenario
    {
        private Sprite center;
        private Sprite topLeft;

        public override void Load(DependencyContainer container)
        {
            base.Load(container);
            
            var texstore = container.Resolve<TextureStore>();
            var tex = texstore.GetResource("ino");

            var fontstore = container.Resolve<FontStore>();
            var font = fontstore.GetResource(SpriteFont.FontTiny);
            
            AddAll(new IDrawable[]
            {
                new SpriteText("Use R to toggle rotation", font)
                {
                    Position = new Vector2(20,600)
                },
                
                // center
                new Container (new IDrawable[]
                {
                    center = new Sprite(tex)
                    {
                        Position = new Vector2(100, 200),
                        Size = new Vector2(200)
                    },    
                    new Box ()
                    {
                        Colour = Color.Red,
                        Position = new Vector2(center.X + center.Width / 2 - 5, center.Y + center.Height / 2 - 5),
                        Size = new Vector2(10)
                    },
                    new SpriteText("center anchor (default)", font)
                    {
                        Position = new Vector2(100,100)
                    },
                }),
                
                // topleft
                new Container (new IDrawable[]
                {
                    topLeft = new Sprite(tex)
                    {
                        Position = new Vector2(700,200),
                        Size = new Vector2(200),
                        Offset = Vector2.Zero
                    },
                    new Box()
                    {
                        Colour = Color.Red,
                        Position = new Vector2(700 - 5, 200 - 5),
                        Size = new Vector2(10)
                    },
                    new SpriteText("topleft anchor (0,0)", font)
                    {
                        Position = new Vector2(700,100)
                    },
                })
            });
        }

        private bool rotate;

        public override void Update()
        {
            base.Update();
            if (rotate)
            {
                center.Rotation += 2f;
                topLeft.Rotation -= 2f;
            }
        }

        public override void KeyDown(KeyArgs key)
        {
            base.KeyDown(key);
            if (key.IsPressed(KeyCode.r))
                rotate = !rotate;
        }
    }
}