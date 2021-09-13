using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;
using Yasai.Extensions;
using Yasai.Graphics.Imaging;

namespace Yasai.Graphics.Text
{
    public class SpriteFont : IResource
    {
        public IntPtr Handle { get; set; }

        private Dictionary<char, IntPtr> glyphs = new Dictionary<char, IntPtr>();

        private char[] characterSet;

        private Game game;
        
        public SpriteFont(Game game, char[] characterSet)
        {
            this.characterSet = characterSet;
            this.game = game;
        }

        /// <summary>
        /// Renders all characters in the character set 
        /// </summary>
        public void LoadGlyphs()
        {
            foreach (char c in characterSet)
            {
                glyphs[c] = SDL.SDL_CreateTextureFromSurface(game.Renderer.GetPtr(),
                    SDL_ttf.TTF_RenderGlyph_Blended(Handle, c, Color4.White.ToSdlColor()));
                
            }
        }

        /// <summary>
        /// reads the internal dictionary and returns a sprite with the glyph drawn on it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Sprite GetGlyph(char c)
        {
            Texture tex = characterSet.Contains(c) ? new Texture(glyphs[c]) : new Texture (glyphs['?']);
            return new Sprite(tex);
        }

        public void Dispose()
        {
            SDL_ttf.TTF_CloseFont(Handle);

            foreach (IntPtr s in glyphs.Values)
            {
                SDL.SDL_DestroyTexture(s); 
            }
        }
    }
}