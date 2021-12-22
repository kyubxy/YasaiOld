using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Yasai.Input.Mouse
{
    public interface IMouseHandler
    {
        // false to block, true to not block
        
        bool MouseClick(Vector2 position, MouseButtonEventArgs buttonArgs);
        bool MouseMove(MouseMoveEventArgs args);
        bool MouseEnter();
        bool MouseExit();
        bool MouseScroll(Vector2 position, MouseWheelEventArgs args);
    }
}