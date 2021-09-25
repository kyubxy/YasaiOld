using Yasai.Graphics.Layout.Groups;

namespace Yasai.Graphics.Layout
{
    public class Screen : Group
    {
        // screenmanagers can only handle one screen at a time
        // thus, they all ignore the hierachy
        public override bool IgnoreHierachy => true;

        public Screen () {}
    }
}