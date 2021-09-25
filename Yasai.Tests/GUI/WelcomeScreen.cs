using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Layout.Screens;
using Yasai.Graphics.Text;
using Yasai.Resources;

namespace Yasai.Tests.GUI
{
    public class WelcomeScreen : Screen
    {
        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            AddAll(new IDrawable[]
            {
                new SpriteText("Welcome to the Yasai testing interface", "fnt_smallFont")
                {
                    Position = new Vector2(20)
                },
                new SpriteText("Press tab to open the scenario picker", "fnt_smallFont")
                {
                    Position = new Vector2(20, 50)
                },
                new SpriteText("This interface serves as the closest thing we have to unit testing.", "fnt_smallFont")
                {
                    Position = new Vector2(20, 70)
                },
                new SpriteText("Just like the things that are being tested this interface is also made with Yasai", "fnt_smallFont")
                {
                    Position = new Vector2(20, 90)
                }
            });
        }
    }
}