using System;
using System.Numerics;
using SharpFNT;
using Yasai.Graphics.Text;
using Yasai.Structures.DI;

namespace Yasai.Resources.Stores
{
    public class FontStore : ContentStore<SpriteFont>
    {
        public override string[] FileTypes => new[] { ".ttf", ".otf" };
        public override IResourceArgs DefaultArgs => new FontArgs(32);

        public FontStore(DependencyContainer container, string root = "Assets") : base(container, root)
        {
        }

        protected override SpriteFont AcquireResource(string path, IResourceArgs args)
        {
            throw new Exception();
            //return new SpriteFont(font, fargs, path);
        }
    }
    
    // might not need args
    public class FontArgs : IResourceArgs
    {
        public int Size { get; } = 31;
        public char[] CharacterSet { get; }

        public FontArgs(int size, char[] cache)
        {
            Size = size;
            CharacterSet = cache;
        }

        public FontArgs(int size) : this() => Size = size;

        public FontArgs()
            => CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567889<>.?,()+ ".ToCharArray();
    }
}