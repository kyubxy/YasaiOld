using System;
using System.IO;
using SDL2;
using Yasai.Graphics.Imaging;

namespace Yasai.Resources.Loaders
{
    public class ImageLoader : ILoader
    {
        public string[] FileTypes => new string[] {".png", ".jpg", ".jpeg", ".webp"};

        public ImageLoader()
        {
            
        }
        
        public IResource GetResource(Game game, string path)
        {
            Texture final = new Texture();

            IntPtr surface = SDL_image.IMG_Load(path);

            if (surface == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError());
            
            final.Handle = SDL.SDL_CreateTextureFromSurface(game.Renderer.GetPtr(), surface);

            return final;
        }
    }
}