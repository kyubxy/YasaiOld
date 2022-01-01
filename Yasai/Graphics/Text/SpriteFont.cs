using System;
using System.Collections.Generic;
using num = System.Numerics;

namespace Yasai.Graphics.Text
{
    /// <summary>
    /// Class manifestation of .fnt file
    /// </summary>
    public class SpriteFont : IFontData, IDisposable
    {
        private Dictionary<char, Glyph> glyphStore;
        
        public bool Bold { get; init; }
        public bool Italic { get; init; }
        public int Size { get; init; }

        public SpriteFont(Dictionary<char, Glyph> glyphStore)
        {
            this.glyphStore = glyphStore;
        }

        /// <summary>
        /// reads the internal store and returns a sprite with the glyph drawn on it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Glyph GetGlyph(char c)
        {
            var ch = glyphStore.ContainsKey(c) ? c : '?';
            return glyphStore[ch];
        }

        public void Dispose()
        {
            foreach (var g in glyphStore.Values)
                g.Dispose();
        }
        
        #region font locations
        public static string Small => "yasai_fontSmall";
        public static string Normal => "yasai_fontNormal";
        public static string Large => "yasai_fontLarge";
        public static string SymbolFontSmall => "yasai_fontSymbolsSmall";
        public static string SymbolFontNormal => "yasai_fontSymbolsNormal";
        public static string SymbolFontLarge => "yasai_fontSymbolsLarge";
        #endregion
    }
}