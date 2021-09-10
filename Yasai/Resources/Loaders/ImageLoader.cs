using System;
using System.IO;
using SDL2;
using Yasai.Graphics.Imaging;

namespace Yasai.Resources.Loaders
{
    public class ImageLoader : ILoader 
    {
        public string[] FileTypes => new string[] {".png", ".jpg", ".jpeg", ".PNG", ".JPEG"};

        public ImageLoader()
        {
            
        }
        
        public IResource GetResource(Game game, string path)
        {
            Texture final = new Texture();

            if (!File.Exists(path))
                throw new FileNotFoundException();

            IntPtr surface = SDL_image.IMG_Load(path);

            if (surface == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError());
            
            final.Handle = SDL.SDL_CreateTextureFromSurface(game.Renderer.GetPtr(), surface);

            return final;
        }
    }
}