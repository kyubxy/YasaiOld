using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;

namespace Yasai.Graphics.Groups
{
    public interface IGroup : IDrawable, IMouseListener, IKeyListener
    {
    }
}