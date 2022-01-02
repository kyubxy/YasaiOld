using System.Drawing;
using OpenTK.Mathematics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Resources.Stores;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Text
{
    public class SpriteText : Container
    {
        public int Spacing { get; set; } = 7;
        
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

        public override void Load(DependencyContainer container)
        {
           base.Load(container);
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
                if (c == ' ')
                {
                    accX += 20;
                }
                else
                {
                    var glyph = Font.GetGlyph(c);
                    var glyphTex = glyph.Texture;
                    Sprite g = new Sprite(glyphTex)
                    {
                        Position = new Vector2(accX, glyph.Offset.Y),
                        Size = new Vector2(glyphTex.Width, glyphTex.Height),
                        Colour = Color.Black
                    };

                    Add(g);
                    accX += g.Size.X;
                }
            }
        }
    }
}