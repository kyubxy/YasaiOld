using Yasai.Graphics.Groups;

namespace Yasai.Screens
{
    public class Screen : Group
    {
        // screenmanagers can only handle one screen at a time
        // thus, they all ignore the hierachy
        public override bool IgnoreHierarchy => true;

        public Screen () {}
    }
}