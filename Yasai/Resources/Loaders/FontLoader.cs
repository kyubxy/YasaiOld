using System;
using Yasai.Graphics.Text;
using SDL2;

namespace Yasai.Resources.Loaders
{
    public class FontLoader : ILoader
    {
        public string[] FileTypes => new string[] {".ttf", ".otf"};
        public IResource GetResource(Game game, string path)
        {
            SpriteFont final = new SpriteFont();
            
            // TODO: support multiple sizes
            final.Handle = SDL_ttf.TTF_OpenFont(path, 32);

            if (final.Handle == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError());

            return final;
        }
    }
}