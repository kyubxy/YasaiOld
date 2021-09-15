using System;
using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Graphics.Primitives
{
    public class Line : Primitive
    {
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }

        public override Vector2 Position { get; set; } = Vector2.Zero;

        private Color4 col;
        
        public override Color4 Colour
        {
            get => col;
            set => col = value;
        }
        
        public override Color4 OutlineColour 
        {
            get => col;
            set => col = value;
        }

        public Line()
        {
        }

        public Line(Vector2 s, Vector2 e)
        {
            StartPosition = s;
            EndPosition = e;
        }

        public override void Draw(IntPtr renderer)
        {
            base.Draw(renderer);
            SDL.SDL_SetRenderDrawColor(renderer,
               (byte) (col.R * 255), (byte) (col.G * 255), (byte) (col.B * 255),
               (byte) (Outline ? Alpha * 255 : 0));

            SDL.SDL_RenderDrawLine(renderer, (int) StartPosition.X + (int) Position.X,
                (int) StartPosition.Y + (int) Position.Y, (int) Position.X + (int) EndPosition.X,
                (int) EndPosition.Y + (int) Position.Y);
        }
    }
}