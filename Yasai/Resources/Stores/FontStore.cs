using System;
using Yasai.Graphics.Text;
using Yasai.Graphics.YasaiSDL;
using Yasai.Structures;
using Yasai.Structures.DI;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace Yasai.Resources.Stores
{
    public class FontStore : ContentStore<SpriteFont>
    {
        private Renderer ren;
        public FontStore(DependencyContainer container, string root = "Assets") : base(container, root) 
            => ren = container.Resolve<Renderer>();

        public override string[] FileTypes => new [] {".ttf", ".otf"};
        public override IResourceArgs DefaultArgs => new FontArgs(32);
        
        protected override SpriteFont AcquireResource(string path, IResourceArgs largs)
        {
            FontArgs args = (FontArgs)largs;
            
            SpriteFont final = new SpriteFont(ren, TTF_OpenFont(path, args.Size), args, path);

            if (final.Handle == IntPtr.Zero)
                throw new Exception(SDL_GetError());

            return final;
        }
    }
    
    public class FontArgs : IResourceArgs
    {
        public int Size { get; } = 32;
        public char[] CharacterSet { get; }

        public FontArgs(int size, char[] cache)
        {
            Size = size;
            CharacterSet = cache;
        }

        public FontArgs(int size) : this() => Size = size;

        public FontArgs()
            => CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890<>.?,()+ ".ToCharArray();
    }
}