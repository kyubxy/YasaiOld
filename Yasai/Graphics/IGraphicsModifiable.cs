using System.Drawing;
using Yasai.Structures;

namespace Yasai.Graphics
{
    public interface IGraphicsModifiable
    {
        public float Alpha { get; set; }
        public Color Colour { get; set; }
    }
}