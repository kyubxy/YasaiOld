using System;
using Yasai.Graphics.Text;
using SDL2;

namespace Yasai.Resources.Loaders
{
    public class FontLoader : ILoader
    {
        public string[] FileTypes => new string[] {".ttf", ".otf"};
        /// <summary>
        /// loads a spritefont
        /// </summary>
        /// <param name="game"></param>
        /// <param name="path"></param>
        /// <param name="args">supported, given in the form size,</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IResource GetResource(Game game, string path, ILoadArgs _args)
        {
            FontArgs args = _args == null ? new FontArgs (): (FontArgs) _args;
            SpriteFont final = new SpriteFont(game, args.CharacterSet);
            final.Handle = SDL_ttf.TTF_OpenFont(path, args.Size);

            if (final.Handle == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError());

            return final;
        }
    }

    public class FontArgs : ILoadArgs
    {
        public int Size { get; } = 32;
        public char[] CharacterSet { get; } 

        public FontArgs(int size, char[] cs)
        {
            Size = size;
            CharacterSet = cs;
        }

        public FontArgs(int size) : this() => Size = size;

        public FontArgs()
        {
            CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890<>.? ".ToCharArray();
        }
    }
}