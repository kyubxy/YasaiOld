using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Screens;
using Yasai.VisualTests.Scenarios;

namespace Yasai.VisualTests.GUI
{
    public class WelcomeScreen : Scenario
    {
        public WelcomeScreen(Game game) 
            : base (game)
        {
            
        }
        
        public override void LoadComplete()
        {
            base.LoadComplete();

            string[] messages = 
            {
                "Press <SHIFT>+<TAB> to open the scenario picker",
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