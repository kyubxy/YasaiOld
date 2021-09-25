using System;
using Yasai.Graphics.Imaging;

using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Yasai.Resources.Loaders
{
    public class ImageLoader : ILoader
    {
        public string[] FileTypes => new string[] {".png", ".jpg", ".jpeg", ".webp"};
        public ILoadArgs DefaultArgs => new EmptyLoadArgs();

        public ImageLoader()
        {
            
        }
        
        public Resource GetResource(Game game, string path, ILoadArgs args)
        {
            if (args != null)
                Console.WriteLine("ImageLoader does not support args");
            
            IntPtr surface = IMG_Load(path);

            if (surface == IntPtr.Zero)
                throw new Exception(SDL_GetError());
            
            return new Texture(SDL_CreateTextureFromSurface(game.Renderer.GetPtr(), surface), path);
        }
    }
}
