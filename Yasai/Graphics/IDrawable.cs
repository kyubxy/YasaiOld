using System;
using Yasai.Resources;
using Yasai.Structures;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IDependencyHolder
    {
        public bool Visible { get; set; }
        public void Draw(IntPtr renderer);
    }
}