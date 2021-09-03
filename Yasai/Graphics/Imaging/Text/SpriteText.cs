using System.Collections.Generic;
using Yasai.Graphics.Layout;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging.Text
{
    // TODO: SpriteText
    public class SpriteText : Group
    {
        private string text;
        public string Text { get; set; }

        private ContentStore glyphs;
        
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