using System.Drawing;
using System.Numerics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Stores;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.GUI
{
    public sealed class StatusBar : Container
    {
        private int HEIGHT => 40;
        
        private readonly Game game;

        private SpriteText title;

        public StatusBar(Game game)
        {
            this.game = game;
            Colour = Color.DarkGray;
            Fill = true;
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            var fontStore = dependencies.Resolve<FontStore>();
            
            Add(title = new SpriteText("", fontStore.GetResource(SpriteFont.FontTiny))
            {
                Colour = Color.Black
            }); 
            updatePositions();
        }

        public override void Update()
        {
            base.Update();
            updatePositions();
        }

        void updatePositions()
        {
            Position = new Vector2(0, game.Window.Height - HEIGHT);
            Size = new Vector2(game.Window.Width, HEIGHT);
            title.Position = new Vector2(10, game.Window.Height - 30);
        }

        public void UpdateTitle(string text) => title.Text = text;
    }
}