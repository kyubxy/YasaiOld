using System.Numerics;
using Yasai.Graphics.Text;
using Yasai.Structures.DI;
using SixLabors.ImageSharp;

namespace Yasai.Resources.Stores
{
    /*
    public class FontStore : ContentStore<SpriteFont>
    {
        public override string[] FileTypes => new[] { ".ttf", ".otf" };
        public override IResourceArgs DefaultArgs => new FontArgs(32);

        private FontCollection collection;
        
        public FontStore(DependencyContainer container, string root = "Assets") : base(container, root)
        {
            collection = new FontCollection();
        }

        protected override SpriteFont AcquireResource(string path, IResourceArgs args)
        {
            var fargs = (FontArgs) args;

            FontFamily family = collection.Install(path);
            Font font = family.CreateFont(fargs.Size, FontStyle.Regular); // TODO: fontstyle

            return new SpriteFont(font, fargs, path);
        }
    }
    
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
    */
}