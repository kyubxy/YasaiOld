using System;
using OpenTK.Windowing.Common;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad //IMouseHandler, IKeyHandler
    {
        //Drawable Parent { get; set; }
        
        bool Visible { get; set; }
        void Draw(FrameEventArgs args);
    }
}