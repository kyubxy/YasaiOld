using System.Collections.Generic;
using Yasai.Graphics.Layout;

namespace Yasai.Graphics.Imaging.Text
{
    public class SpriteText : Group
    {
        private string text;
        public string Text { get; set; }

        private Dictionary<char, Sprite> glyphs;
        
        public SpriteText()
        {
        }

        public SpriteText(string text)
        {
        }

        public void Load()
        {
        }
    }
}