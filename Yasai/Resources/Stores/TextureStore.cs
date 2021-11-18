using System;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.YasaiSDL;
using Yasai.Structures;
using Yasai.Structures.DI;
using static SDL2.SDL_image;
using static SDL2.SDL;

namespace Yasai.Resources.Stores
{
    public class TextureStore : ContentStore<Texture>
    {
        private Renderer renderer;

        public TextureStore(DependencyContainer container, string root = "Assets")
            : base(container, root) => renderer = container.Resolve<Renderer>();

        public override string[] FileTypes => new [] {".png", ".jpg", ".jpeg", ".webp"};
        public override IResourceArgs DefaultArgs => new EmptyResourceArgs();
        
        protected override Texture AcquireResource(string path, IResourceArgs args)
        {
            if (args != null)
                Console.WriteLine("ImageLoader does not support args");
            
            IntPtr surface = IMG_Load(path);

            if (surface == IntPtr.Zero)
                throw new Exception(SDL_GetError());
            
            return new Texture(SDL_CreateTextureFromSurface(renderer.GetPtr(), surface), path);
        }
    }
}