using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Yasai.Resources;
using Yasai.Extensions;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.YasaiSDL;
using Yasai.Resources.Stores;

using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace Yasai.Graphics.Text
{
    public class SpriteFont : Resource
    {
        public static string TinyFont => "yasai_fontTiny";

        private Dictionary<char, Sprite> glyphs = new Dictionary<char, Sprite>();

        private char[] characterSet;

        private Renderer renderer;
        
        public SpriteFont(Renderer ren, IntPtr h, FontArgs args, string path) : base(h, path, args)
        {
            characterSet = args.CharacterSet;
            renderer = ren;
        }

        /// <summary>
        /// Renders all characters in the character set 
        /// </summary>
        public void LoadGlyphs()
        {
            foreach (char c in characterSet)
            {
                IntPtr t = SDL_CreateTextureFromSurface(renderer.GetPtr(), 
                    TTF_RenderGlyph_Blended(Handle, c, Color.White.ToSdlColor()));
                
                glyphs[c] = new Sprite(new Texture(t));
            }
        }

        /// <summary>
        /// reads the internal dictionary and returns a sprite with the glyph drawn on it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Sprite GetGlyph(char c)
        {
            return characterSet.Contains(c) ? glyphs[c] : glyphs['?'];
        }

        public override void Dispose()
        {
            base.Dispose();
            TTF_CloseFont(Handle);
        }
    }
}
