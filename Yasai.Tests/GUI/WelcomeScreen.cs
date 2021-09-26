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

            string[] messages = 
            {
                "Press <TAB> to open the scenario picker",
                "",
                "Youre probably here because you havent opened a test screen yet.",
                "This interface serves as the closest thing we have to unit testing.",
                "Just like the things that are being tested this interface is also made with Yasai"
            };

            int i = 0;
            foreach (string msg in messages)
            {

                Add(new SpriteText(msg, "fnt_smallFont")
                {
                    Position = new Vector2(20, 80 + i * 20)
                });
                i++;
            }
            
            AddAll(new IDrawable[]
            {
                new SpriteText("Welcome to the Yasai visual testing interface", "fnt_smallFont")
                {
                    Position = new Vector2(20)
                },
            });
        }
    }
}