using System;
using System.Collections.Generic;
using System.IO;
using SharpFNT;
using Yasai.Graphics.Text;
using Yasai.Structures.DI;

namespace Yasai.Resources.Stores
{
    public class FontStore : ContentStore<SpriteFont>
    {
        public override string[] FileTypes => new[] { ".fnt" };
        public override IResourceArgs DefaultArgs => new FontArgs(32);

        private DependencyContainer container;
        
        public FontStore(DependencyContainer container, string root = "Assets") : base(container, root)
        {
            this.container = container;
        }

        protected override SpriteFont AcquireResource(string path, IResourceArgs args)
        {
            // store the glyphs somewhere
            TextureStore glyphs = new TextureStore(container);

            // this goes in the spritesheet data 
            var dict = new Dictionary<string, SpritesheetData.Tile>();

            // actual file
            BitmapFont bmfont = BitmapFont.FromFile(path);

            // put the glyph data into the dictionary 
            foreach (var pair in bmfont.Characters)
            {
                var glyphBox = pair.Value;
                var tile = new SpritesheetData.Tile(glyphBox.X, glyphBox.Y, glyphBox.Width, glyphBox.Height);
                var glyphName = ((char) pair.Key).ToString();
                dict[glyphName] = tile;
            }

            // put the dictionary into the sheet data
            SpritesheetData sheetData = new SpritesheetData(dict);

            // load each page 
            // !! the function already includes the root dir
            foreach (var page in bmfont.Pages.Values)
                glyphs.LoadSpritesheet(page, sheetData);

            // return the font
            return new SpriteFont(glyphs, (FontArgs)DefaultArgs, path);
        }
    }
    
    // might not need args
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
            => CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567889<>.?,()+ ".ToCharArray();
    }
}