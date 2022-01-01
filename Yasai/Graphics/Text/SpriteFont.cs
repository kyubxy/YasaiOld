using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using SharpFNT;
using Yasai.Resources;
using Yasai.Graphics.Imaging;
using Yasai.Resources.Stores;
using num = System.Numerics;

namespace Yasai.Graphics.Text
{
    /// <summary>
    /// Class manifestation of .fnt file
    /// </summary>
    public class SpriteFont : IDisposable
    {
        #region font locations
        public static string Small => "yasai_fontSmall";
        public static string Normal => "yasai_fontNormal";
        public static string Large => "yasai_fontLarge";
        public static string SymbolFontSmall => "yasai_fontSymbolsSmall";
        public static string SymbolFontNormal => "yasai_fontSymbolsNormal";
        public static string SymbolFontLarge => "yasai_fontSymbolsLarge";
        #endregion
        
        private TextureStore glyphStore;

        public SpriteFont(TextureStore store)
        {
            glyphStore = store;
        }

        /// <summary>
        /// reads the internal store and returns a sprite with the glyph drawn on it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Texture GetGlyph(char c)
        {
            // TODO:
            //var ch = chars.Contains(c) ? c.ToString() : "?";
            return glyphStore.GetResource(c.ToString());
        }

        public void Dispose()
        {
        }
    }
}