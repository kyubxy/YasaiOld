using System.Collections.Generic;
using Yasai.Graphics.Layout;
using Yasai.Resources;

namespace Yasai.Graphics.Text
{
    public class SpriteText : Group
    {
        private string text = "";
        public string Text { get; set; }

        private ContentStore glyphs;
        
        public SpriteText() : this ("")
        {
        }

        public SpriteText(string text)
        {
            Text = text;
        }

        public override void Load(ContentStore cs)
        {
            base.Load(cs);
        }
    }
}