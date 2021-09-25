using System.Numerics;
using static SDL2.SDL;

namespace Yasai.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Converts a <see cref="OpenTK.Mathematics.Vector2"/> to a <see cref="SDL_Point"/>.
        /// Mind the integer conversion
        /// </summary>
        /// <param name="v">the vector to convert</param>
        /// <returns>the equivalent SDL point</returns>
        public static SDL_Point ToSdlPoint(this Vector2 v)
        {
            SDL_Point ret;
            ret.x = (int)v.X;
            ret.y = (int)v.Y;
            return ret;
        }

    }
}