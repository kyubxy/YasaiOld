using OpenTK.Mathematics;
using SLIS = SixLabors.ImageSharp;

namespace Yasai.Graphics
{
    public struct Rectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public Vector2 Position => new (X, Y);
        public Vector2 Size => new(Width, Height);

        public Rectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public Rectangle(Vector2 position, Vector2 size) 
            : this(position.X, position.Y, size.X, size.Y) 
        { }

        internal SLIS.Rectangle ToImageSharp() => new((int)X, (int)Y, (int)Width, (int)Height);
        internal SLIS.RectangleF ToImageSharpF() => new(X, Y, Width, Height);
    }
}