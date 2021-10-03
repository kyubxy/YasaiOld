using System.Numerics;
using System.Drawing;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario("wangs")]
    public class DrawablePropertyTest : Scenario
    {
        private Sprite s;
        public DrawablePropertyTest(Game g) : base (g)
        {
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

        public override void Start(ContentCache cache)
        {
            cache.LoadResource("ino.png");
            base.Start(cache);
        }

        public override void Update()
        {
            s.Rotation += 2f;
            base.Update();
        }
    }
}