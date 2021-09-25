using System.Drawing;
using static SDL2.SDL;

namespace Yasai.Extensions
{
    public static class ColourExtensions
    {
        /// <summary>
        /// Converts a <see cref="Colour"/> to a <see cref="SDL_Color"/>
        /// </summary>
        /// <param name="c">the colour to convert</param>
        /// <returns>the converted colour</returns>
        public static SDL_Color ToSdlColor(this Color c)
        {
            SDL_Color final = default;
            final.r = c.R;
            final.g = c.G;
            final.b = c.B;
            return final;
        }
    }
}