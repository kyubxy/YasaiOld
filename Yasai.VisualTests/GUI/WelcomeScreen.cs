using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;
using Yasai.VisualTests.Scenarios;

namespace Yasai.VisualTests.GUI
{
    public class WelcomeScreen : Scenario
    {
        public WelcomeScreen(Game game) 
            : base (game)
        { }
        
        public override void Load (DependencyContainer container)
        {
            base.Load(container);

            var fontStore = container.Resolve<FontStore>();

            string[] messages = 
            {
                "Press <SHIFT>+<TAB> to open the scenario picker",
                "",
                "You're probably here because you havent opened a test scenario yet.",
                "This interface serves as the a means of testing components in a visual fashion.",
                "Just like the things that are being tested this interface is also made with Yasai"
            };

            int i = 0;
            foreach (string msg in messages)
            {
                Add(new SpriteText(msg, fontStore.GetResource(SpriteFont.FontTiny))
                {
                    Position = new Vector2(20, 80 + i * 20)
                });
                i++;
            }
            
            Add(new SpriteText("Welcome to the Yasai visual testing interface", fontStore.GetResource(SpriteFont.FontTiny)) 
            {
                Position = new Vector2(20)
            });
        }
    }
}