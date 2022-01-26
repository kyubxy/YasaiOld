using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpFNT;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Text;
using Rectangle = Yasai.Graphics.Rectangle;

namespace Yasai.Resources.Stores
{
    public class FontStore : Store<SpriteFont>
    {
        public override string[] FileTypes => new[] { ".fnt" };
        public override IResourceArgs DefaultArgs => new EmptyResourceArgs();

        public FontStore(string root = "Assets") : base(root)
        { }

        protected override SpriteFont AcquireResource(string path, IResourceArgs args)
        {
            var glyphStore = new Dictionary<char, Glyph>();
            
            // actual file
            BitmapFont bmfont = BitmapFont.FromFile(path);

            Image<Rgba32>[] pages = new Image<Rgba32>[bmfont.Pages.Count];
            
            // load each font sheet first
            // !! the function already includes the root dir
            for (int i = 0; i < pages.Length; i++)
            {
                var pageLoc = bmfont.Pages[i];
                pages[i] = Image.Load<Rgba32>(Path.Combine(Root, pageLoc));
            }
            
            // put the glyph data into the dictionary 
            foreach (var pair in bmfont.Characters)
            {
                // location of glyph
                var glyph = pair.Value;
                Rectangle area = new Rectangle(glyph.X, glyph.Y, glyph.Width, glyph.Height);
                
                // character
                char glyphName = (char) pair.Key;

                glyphStore[glyphName] = new Glyph(ImageHelpers.LoadSectionFromTexture(pages[glyph.Page], area, TextureMinFilter.Nearest, TextureMagFilter.Nearest))
                {
                    Offset = new Vector2i(glyph.XOffset, glyph.YOffset),
                    XAdvance = glyph.XAdvance,
                };
            }

            // return the font
            return new SpriteFont(glyphStore);
        }
    }
}