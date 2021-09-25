using System.Collections.Generic;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics.Layout.Groups
{
    public interface IGroup : IDrawable, IMouseListener, IKeyListener 
    {
    }
}