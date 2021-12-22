using System.Drawing;
using OpenTK.Mathematics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Text
{
    public class SpriteText : Container
    {
        public string Text
        {
            get => BindableText.Value;
            set => BindableText.Value = value;
        }

        public readonly Bindable<string> BindableText = new ();

        public SpriteFont Font { get; protected set; }

        public override bool Loaded => Font != null && base.Loaded;

        public SpriteText() 
        {
        }
        
        public SpriteText(string text, SpriteFont font)
        {
            Text = text;
            Font = font;

            BindableText.OnChanged += s => updateText();
        }

        public override void Load(DependencyContainer dependencies)
        {
           base.Load(dependencies);
           updateText();
        }

        private void updateText()
        {
            if (!Loaded)
                return;
            
            char[] chars = Text.ToCharArray();
            
            // TODO: only change the changed characters
            Clear();

            float accX = 0;
            foreach (char c in chars)
            {
                Sprite g = new Sprite(Font.GetGlyph(c))
                {
                    Position = new Vector2(accX, 0),
                    Colour = Colour,
                };
                
                Add(g);
                accX += g.Size.X;
            }
        }
    }
}