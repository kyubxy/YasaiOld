using SDL2;
using Yasai.Input.Keyboard;

namespace Yasai.Extensions
{
    public static class KeyCodeExtensions
    {
        public static SDL.SDL_Keycode ToSdlKeyCode(this KeyCode k) => (SDL.SDL_Keycode)(k);
        public static KeyCode ToYasaiKeyCode(this SDL.SDL_Keycode k) => (KeyCode)(k);
    }
}