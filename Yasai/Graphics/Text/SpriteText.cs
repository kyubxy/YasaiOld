using System;
using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Layout;
using Yasai.Resources;

namespace Yasai.Graphics.Text
{
    public class SpriteText : Group
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

        public SpriteFont Font { get; protected set; }

        public override bool Loaded => Font != null && Font.Handle != IntPtr.Zero;

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

        private string _fontLoc;
        public SpriteText(string text, string font)
        {
            Text = text;
            _fontLoc = font;
        }

        public override void Load(ContentStore cs)
        {
            if (!Loaded) 
                Font = cs.GetResource<SpriteFont>(_fontLoc);
            Font.LoadGlyphs();
            
            base.Load(cs);
            
            updateText();
        }

        private void updateText()
        {
            char[] chars = Text.ToCharArray();
            
            // TODO: only change the changed characters
            Clear();

            float accX = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                // TODO: COPY THE SPRITES GOT DAMN
                Sprite g = new Sprite(Font.GetGlyph(chars[i]).CurrentTexture);
                g.Position = new Vector2 (Position.X + accX, Position.Y);
                accX += g.Size.X;
                Add(g);
            }
        }
    }
}