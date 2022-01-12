using OpenTK.Mathematics;

namespace Yasai.Graphics.Primitives
{
    public abstract class Quad : Primitive
    {
        protected abstract Vector2 TopRightVertex { get; }
        protected abstract Vector2 TopLeftVertex { get; }
        protected abstract Vector2 BottomRightVertex { get; }
        protected abstract Vector2 BottomLeftVertex { get; }
        
        protected sealed override float[] Vertices => new []
        {
            // Position                                Texture
            TopRightVertex.X,  TopRightVertex.Y,       0.0f, 1.0f, 0.0f, // top right
            BottomRightVertex.X, BottomRightVertex.Y,  0.0f, 1.0f, 1.0f, // bottom right
            BottomLeftVertex.X, BottomLeftVertex.Y,    0.0f, 0.0f, 1.0f, // bottom left
            TopLeftVertex.X, TopLeftVertex.Y,          0.0f, 0.0f, 0.0f  // top left
        };
    
        public sealed override uint[] Indices => new uint[]
        {
            0, 1, 3, 
            1, 2, 3 
        };
    }
}