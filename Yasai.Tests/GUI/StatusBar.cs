using System.Drawing;
using System.Numerics;
using Yasai.Graphics.Layout;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Resources;

namespace Yasai.Tests.GUI
{
    public class StatusBar : Group
    {
        private int HEIGHT => 40;
        
        private readonly Game game;


        private Box back;
        private SpriteText title;

        public StatusBar(Game game)
        {
            this.game = game;
            Colour = Color.DarkGray;
            Fill = true;
        }
        
        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            Add(title = new SpriteText("", "fnt_smallFont")
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