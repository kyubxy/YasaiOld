using System;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IGeometry, IGraphicsModifiable
    {
        public bool Visible { get; set; }
        public void Draw(IntPtr renderer);
    }
}