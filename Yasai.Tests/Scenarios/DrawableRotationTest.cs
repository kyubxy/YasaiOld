using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class DrawableRotationTest : Scenario
    {
        private Sprite center;
        private Sprite topLeft;
        
        public DrawableRotationTest(Game g) : base (g) { }
        
        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            cache.LoadResource("ino.png");

            AddAll(new IDrawable[]
            {
                new SpriteText("Use R to toggle rotation", "fnt_smallFont")
                {
                    Position = new Vector2(20,600)
                },
                
                // center
                new Group (new IDrawable[]
                {
                    center = new Sprite("ino")
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
                    new SpriteText("center anchor (default)", "fnt_smallFont")
                    {
                        Position = new Vector2(100,100)
                    },
                }),
                
                // topleft
                new Group (new IDrawable[]
                {
                    topLeft = new Sprite("ino")
                    {
                        Position = new Vector2(700,200),
                        Size = new Vector2(200),
                        Origin = Vector2.Zero
                    },
                    new Box()
                    {
                        Colour = Color.Red,
                        Position = new Vector2(700 - 5, 200 - 5),
                        Size = new Vector2(10)
                    },
                    new SpriteText("topleft anchor (0,0)", "fnt_smallFont")
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