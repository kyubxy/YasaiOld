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
        public static string Segoe => "yasai_fontSegoe";
        #endregion
    }
}