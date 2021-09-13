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

        private Dictionary<char, Sprite> glyphs = new Dictionary<char, Sprite>();

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
                Texture tex = new Texture();
                tex.Handle = SDL.SDL_CreateTextureFromSurface(game.Renderer.GetPtr(), 
                    SDL_ttf.TTF_RenderGlyph_Blended(Handle, c, Color4.White.ToSdlColor()));
                
                glyphs[c] = new Sprite(tex);
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

        public void Dispose()
        {
            SDL_ttf.TTF_CloseFont(Handle);

            foreach (Sprite s in glyphs.Values)
            {
                s.Dispose();
            }
        }
    }
}