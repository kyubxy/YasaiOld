using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class DrawablePropertyTest : Scenario
    {
        private Sprite s;
        public DrawablePropertyTest()
        {
            Add(new Sprite("ino")
            {
                Position = new Vector2(0,0),
                Size = new Vector2(100, 100),
                Flip = Flip.Horizontal,
                Colour = Color4.CornflowerBlue
            });
            
            Add(new Sprite("ino")
            {
                Position = new Vector2(400,0),
                Size = new Vector2(100, 100),
                Flip = Flip.Vertical,
                Colour = Color4.CornflowerBlue,
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

        public override void Load(ContentCache cache)
        {
            cache.LoadResource("ino.png");
            base.Load(cache);
        }

        public override void Update()
        {
            s.Rotation += 0.05f;
            base.Update();
        }
    }
}