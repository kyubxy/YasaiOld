using OpenTK.Mathematics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Text
{
    public class SpriteText : Container, IText
    {
        public int WordSpacing { get; set; } = 20;
        public int Spacing { get; set; } = 0;
        public float CharScale { get; set; } = 1f;
        public Align TextAlign { get; set; } = Align.Left;

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

            BindableText.OnChanged += s => redrawText();
        }

        public override void Load(DependencyContainer container)
        {
           base.Load(container);
           redrawText();
        }

        public float TextWidth
        {
            get
            {
                float ret = 0;
                
                foreach (char c in Text)
                {
                    if (c == ' ')
                        ret += WordSpacing + Spacing;
                    else
                        ret += Font.GetGlyph(c).Texture.Width * CharScale + Spacing;
                }

                return ret;
            }
        }
        
        private void redrawText()
        {
            if (!Loaded)
                return;
            
            char[] chars = Text.ToCharArray();
            
            Clear();
            
            float accX = 0;
            foreach (char c in chars)
            {
                if (c == ' ')
                {
                    accX += WordSpacing + Spacing;
                }
                else
                {
                    var glyph = Font.GetGlyph(c);
                    var glyphTex = glyph.Texture;
                    var g = new Sprite(glyphTex)
                    {
                        Position = new Vector2(accX - TextWidth * ((int)TextAlign / 2f), glyph.Offset.Y * CharScale),
                        Size = new Vector2(glyphTex.Width * CharScale, glyphTex.Height * CharScale),
                        Colour = Colour
                    };

                    Add(g);
                    accX += g.Size.X + Spacing;
                }
            }
        }
    }
}