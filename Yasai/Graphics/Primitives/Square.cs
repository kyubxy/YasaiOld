using OpenTK.Mathematics;

namespace Yasai.Graphics.Primitives
{
    public class Square : Quad
    {
        protected sealed override Vector2 TopRightVertex => new(1, 1);
        protected sealed override Vector2 TopLeftVertex => new(-1,1);
        protected sealed override Vector2 BottomRightVertex => new(1,-1);
        protected sealed override Vector2 BottomLeftVertex => new(-1, -1);
    }
}