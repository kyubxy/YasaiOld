using System.Drawing;
using OpenTK.Mathematics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Text
{
    /*
    public class SpriteText : Container
    {
        private string text = "";
        public string Text
        {
            get => text;
            set
            {
                text = value;
                if (Loaded)
                    updateText();
            }
        }

        private Vector2 position;
        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                updatePositions();
            }
        }

        public SpriteFont Font { get; protected set; }

        public override Color Colour { get; set; } = Color.White;

        public override bool Loaded => Font != null && base.Loaded;

        public SpriteText() 
        {
        }

        public SpriteText(string text)  
        {
            Text = text;
        }

        public SpriteText(string text, SpriteFont font)
        {
            Text = text;
            Font = font;
        }

        public override void Load(DependencyContainer dependencies)
        {
           base.Load(dependencies);
           Font.LoadGlyphs();
           updateText();
        }
       
        private void updatePositions()
        {
            float accX = 0;
            foreach (var drawable in this)
            {
                var g = (Sprite)drawable;
                g.Position = new Vector2 (Position.X + accX, Position.Y);
                g.Colour = Colour;
                accX += g.Size.X;
            }
        }

        private void updateText()
        {
            char[] chars = Text.ToCharArray();
            
            // TODO: only change the changed characters
            Clear();

            foreach (char c in chars) 
                Add(new Sprite(Font.GetGlyph(c)));
            
            updatePositions();
        }
    }
    */
}