using System;
using Yasai.Debug.Logging;
using static SDL2.SDL;

namespace Yasai.Graphics.YasaiSDL
{
    public class Window
    {
        private IntPtr window;

        public Window(string title, int w = 1366, int h = 768, int refreshRate = 60)
        {
            window = SDL_CreateWindow(title, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, w, h, SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            SDL_DisplayMode dm = new SDL_DisplayMode()
            {
                w = w, 
                h = h,
                refresh_rate = refreshRate
            };

            SDL_SetWindowDisplayMode(window, ref dm);
            
            if (window == IntPtr.Zero)
                GameBase.YasaiLogger.LogError($"error on window creation: {SDL_GetError()}");
        }

        // wrapping SDL function
        public void SetTitle(string title) => SDL_SetWindowTitle(window, title);
        public void SetFullscreen(bool fullscreen) => SDL_SetWindowFullscreen(window, (uint) (fullscreen ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE));
        public void SetBrightness(float brightness) => SDL_SetWindowBrightness(window, brightness);
        public void SetOpacity(float opacity) => SDL_SetWindowOpacity(window, opacity);
        public void SetPosition(int x, int y) => SDL_SetWindowPosition(window, x, y);
        public void SetSize(int w, int h) => SDL_SetWindowSize(window, w, h);

        public int Width
        {
            get
            {
                int w;
                SDL_GetWindowSize(window, out w, out _);
                return w;
            }
        }

        public int Height
        {
            get
            {
                int h;
                SDL_GetWindowSize(window, out _, out h);
                return h;
            }
        }

        public IntPtr GetPtr() => window;
    }
}