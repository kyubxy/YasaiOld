using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Converts a <see cref="OpenTK.Mathematics.Vector2"/> to a <see cref="SDL.SDL_Point"/>.
        /// Mind the integer conversion
        /// </summary>
        /// <param name="v">the vector to convert</param>
        /// <returns>the equivalent SDL point</returns>
        public static SDL.SDL_Point ToSdlPoint(this Vector2 v)
        {
            SDL.SDL_Point ret;
            ret.x = (int)v.X;
            ret.y = (int)v.Y;
            return ret;
        }

        /// <summary>
        /// Converts a <see cref="OpenTK.Mathematics.Color4"/> to a <see cref="SDL.SDL_Color"/>
        /// </summary>
        /// <param name="c">the colour to convert</param>
        /// <returns>the converted colour</returns>
        public static SDL.SDL_Color ToSdlColor(this Color4 c)
        {
            SDL.SDL_Color final = default;
            final.r = (byte) (255 * c.R);
            final.g = (byte) (255 * c.G);
            final.b = (byte) (255 * c.B);
            return final;
        }
    }
}