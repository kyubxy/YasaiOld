using System;
using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Graphics.Primitives
{
    /// <summary>
    /// A box drawn using <see cref="SDL.SDL_RenderDrawRect"/> and its fill variant.
    /// As this class inherits <see cref="Primitive"/>, additional <see cref="Drawable"/> functionality
    /// is not supported. For a box with more functionality, use <see cref="Box"/>
    /// </summary>
    public class PrimitiveBox : Primitive
    {
        public override void Draw(IntPtr renderer)
        {

            SDL.SDL_Rect r = new SDL.SDL_Rect
            {
                x = (int)Position.X,
                y = (int)Position.Y,
                w = (int)Size.X,
                h = (int)Size.Y
            };
            
            base.Draw(renderer);
            
            if (Fill)
                SDL.SDL_RenderFillRect(renderer, ref r);
            else
                SDL.SDL_RenderDrawRect(renderer, ref r);
        }
    }
}