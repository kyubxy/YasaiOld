using OpenTK.Windowing.Common;

namespace Yasai.Input.Keyboard
{
    public interface IKeyboardHandler
    {
        void KeyDown(KeyboardKeyEventArgs args);
        void KeyUp(KeyboardKeyEventArgs args);
    }
}