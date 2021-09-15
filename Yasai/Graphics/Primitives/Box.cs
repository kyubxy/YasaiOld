using System;
using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Graphics.Primitives
{
    public class Box : Drawable
    {
        public bool Fill = true;
        
        public override void Draw(IntPtr renderer)
        {
            if (!Enabled)
                return;
            
            base.Draw(renderer);

            SDL.SDL_Rect r = new SDL.SDL_Rect
            {
                x = (int)Position.X,
                y = (int)Position.Y,
                w = (int)Size.X,
                h = (int)Size.Y
            };
            
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            if (Fill)
                SDL.SDL_RenderFillRect(renderer, ref r);
            else
                SDL.SDL_RenderDrawRect(renderer, ref r);
        }
    }
}