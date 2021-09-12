using System;
using System.Collections.Generic;
using System.IO;
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

        public SpriteFont(Game game, int startIndex, int endIndex)
        {
            characterSet = new char [endIndex - startIndex];
            for (int i = startIndex; i < endIndex; i++)
            {
                characterSet[i] = (char) i;
            }

            this.game = game;
        }

        /// <summary>
        /// Renders all characters in the character set 
        /// </summary>
        public void LoadGlyphs()
        {
            foreach (char c in characterSet)
            {
                IntPtr surf;
                glyphs[c] = SDL.SDL_CreateTextureFromSurface(game.Renderer.GetPtr(),
                    SDL_ttf.TTF_RenderGlyph_Solid(Handle, c, Color4.White.ToSdlColor()));
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