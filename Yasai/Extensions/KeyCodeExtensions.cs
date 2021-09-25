using Yasai.Input.Keyboard;
using static SDL2.SDL;

namespace Yasai.Extensions
{
    public static class KeyCodeExtensions
    {
        public static SDL_Keycode ToSdlKeyCode(this KeyCode k) => (SDL_Keycode)(k);
        public static KeyCode ToYasaiKeyCode(this SDL_Keycode k) => (KeyCode)(k);
    }
}