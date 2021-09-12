using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public interface IGraphicsModifiable
    {
        public float Alpha { get; set; }
        public Color4 Colour { get; set; }
    }
}