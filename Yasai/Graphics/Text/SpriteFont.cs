using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SharpFNT;
using Yasai.Resources;
using Yasai.Graphics.Imaging;
using Yasai.Resources.Stores;
using num = System.Numerics;

namespace Yasai.Graphics.Text
{
    /*
    public class SpriteFont : Resource<Font>
    {
        public static string FontTiny => "yasai_fontTiny";
        public static string SymbolFontTiny => "yasai_fontSymbols";

        private Dictionary<char, Texture> glyphs = new ();

        private Font font;
        private char[] characterSet;

        public SpriteFont(Font f, FontArgs args, string path) : base(f, path, args)
        {
            font = f;
            characterSet = args.CharacterSet;
        }

        int toOne(float x) => (int) (x < 1 ? 1 : x);
        
        /// <summary>
        /// Renders all characters in the character set 
        /// </summary>
        public void LoadGlyphs()
        {
            // use font data to read teh spritesheet
            
            foreach (char c in characterSet)
            {
                int p = c;
                //var box = font.GetGlyph(p).BoundingBox(num.Vector2.Zero, new num.Vector2(5));
                /*
                using (Image<Rgba32> image = new Image<Rgba32>(toOne(box.Width), toOne(box.Height)))
                {
                    image.Mutate(x => x.DrawText(c.ToString(), font, Color.White, new PointF(0, 0)));
                    
                    // TODO: this image processing code needs to be abstracted..
                    // similar occurence in TextureStore
                    IntPtr handle = (IntPtr)GL.GenTexture();
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, (int)handle);
                    
                    var pixels = new List<byte>(4 * image.Width * image.Height);

                    for (int y = 0; y < image.Height; y++) 
                    {
                        var row = image.GetPixelRowSpan(y);

                        for (int x = 0; x < image.Width; x++) 
                        {
                            pixels.Add(row[x].R);
                            pixels.Add(row[x].G);
                            pixels.Add(row[x].B);
                            pixels.Add(row[x].A);
                        }
                    }
                    
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
                        PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
                    
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                    glyphs[c] = new Texture(handle);
                }
            }
        }

        /// <summary>
        /// reads the internal dictionary and returns a sprite with the glyph drawn on it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Texture GetGlyph(char c)
            => ((IList) characterSet).Contains(c) ? glyphs[c] : glyphs['?'];
    }
    */
}