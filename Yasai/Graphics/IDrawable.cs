using System;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IMouseHandler, IKeyHandler
    {
        Drawable Parent { get; set; }
        
        bool Visible { get; set; }
        void Draw(IntPtr renderer);
    }
}