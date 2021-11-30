namespace Yasai.Graphics.Primitives
{
    public class Quad : Primitive
    {
        public override float[] Vertices => new []
        {
            // Position         Texture
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 1.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 0.0f  // top left
        };
    
        public override uint[] Indices => new uint[]
        {
            0, 1, 3, 
            1, 2, 3 
        };
    }
}