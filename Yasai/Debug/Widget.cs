using System;
using System.Drawing;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Resources;

namespace Yasai.Debug
{
    public class Widget : Group
    {
        public override bool IgnoreHierarchy => true;

        protected Color BACKGROUND_COLOUR = Color.DimGray;

        public Widget()
        {
        }
    }
}