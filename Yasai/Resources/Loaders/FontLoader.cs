using System;
using Yasai.Graphics.Text;

using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace Yasai.Resources.Loaders
{
    public class FontLoader : ILoader
    {
        public string[] FileTypes => new string[] {".ttf", ".otf"};

        public ILoadArgs DefaultArgs => new FontArgs(32);

        /// <summary>
        /// loads a spritefont
        /// </summary>
        /// <param name="game"></param>
        /// <param name="path"></param>
        /// <param name="args">supported, given in the form size,</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Resource GetResource(Game game, string path, ILoadArgs _args)
        {
            FontArgs args = _args == null ? new FontArgs() : (FontArgs) _args;
            SpriteFont final = new SpriteFont(game, TTF_OpenFont(path, args.Size), args, path);

            if (final.Handle == IntPtr.Zero)
                throw new Exception(SDL_GetError());

            return final;
        }
    }

    public class FontArgs : ILoadArgs
    {
        public int Size { get; } = 32;
        public char[] CharacterSet { get; }

        public FontArgs(int size, char[] cache)
        {
            Size = size;
            CharacterSet = cache;
        }

        public FontArgs(int size) : this() => Size = size;

        public FontArgs()
        {
            CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890<>.? ".ToCharArray();
        }
    }
}
