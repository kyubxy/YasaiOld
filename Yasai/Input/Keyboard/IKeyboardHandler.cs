using System;
using OpenTK.Windowing.Common;

namespace Yasai.Input.Keyboard
{
    public interface IKeyboardHandler
    {
        void KeyDown(KeyboardKeyEventArgs args);
        void KeyUp(KeyboardKeyEventArgs args);

        event Action<KeyboardKeyEventArgs> KeyDownEvent;
        event Action<KeyboardKeyEventArgs> KeyUpEvent;
    }
}