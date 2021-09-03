using System;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IGeometry
    {
        // TODO: add alpha, colour
        public bool Visible { get; set; }
        public void Draw(IntPtr renderer);
    }
}