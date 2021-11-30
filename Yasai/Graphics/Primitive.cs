namespace Yasai.Graphics
{
    public abstract class Primitive : Drawable
    {
        public abstract float[] Vertices { get; }
        public abstract uint[] Indices { get; }
    }
}