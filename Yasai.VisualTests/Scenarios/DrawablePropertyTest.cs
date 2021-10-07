using System.Numerics;
using System.Drawing;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.VisualTests.Scenarios
{
    [TestScenario("wangs")]
    public class DrawablePropertyTest : Scenario
    {
        private Sprite s;
        public DrawablePropertyTest(Game g) : base (g)
        {
            
        }

        public override void Load(ContentCache cache)
        {
            cache.LoadResource("ino.png");
            base.Load(cache);
        }

        public override void LoadComplete()
        {
            base.LoadComplete();
            
            Add(new Sprite("ino")
            {
                Position = new Vector2(0,0),
                Size = new Vector2(100, 100),
                Flip = Flip.Horizontal,
                Colour = Color.CornflowerBlue
            });
            
            Add(new Sprite("ino")
            {
                Position = new Vector2(400,0),
                Size = new Vector2(100, 100),
                Flip = Flip.Vertical,
                Colour = Color.CornflowerBlue,
                Alpha = 0.5f,
            });           
            
            Add(new Sprite("ino")
            {
                Position = new Vector2(0,400),
                Alpha = 0.5f
            });           
            
            Add(s = new Sprite("ino")
            {
                Position = new Vector2(400,400),
                Size = new Vector2(100, 100),
                Flip = Flip.Horizontal
            });
        }

        public override void Update()
        {
            base.Update();
            s.Rotation += 2f;
        }
    }
}