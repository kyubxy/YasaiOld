namespace Yasai.Graphics.Primitives
{
    public class Quad : Primitive
    {
        protected sealed override float[] Vertices => new []
        {
            // Position         Texture
             1.0f,  1.0f, 0.0f, 1.0f, 0.0f, // top right
             1.0f, -1.0f, 0.0f, 1.0f, 1.0f, // bottom right
            -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, // bottom left
            -1.0f,  1.0f, 0.0f, 0.0f, 0.0f  // top left
        };
    
        public sealed override uint[] Indices => new uint[]
        {
            0, 1, 3, 
            1, 2, 3 
        };
    }
}