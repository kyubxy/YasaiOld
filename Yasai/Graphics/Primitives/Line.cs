using System;
using System.Numerics;

using static SDL2.SDL;

namespace Yasai.Graphics.Primitives
{
    public class Line : Primitive
    {
        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }

        public override Vector2 Position { get; set; } = Vector2.Zero;

        public override bool Fill => true;

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

            SDL_RenderDrawLine(renderer, (int) StartPosition.X + (int) Position.X,
                (int) StartPosition.Y + (int) Position.Y, (int) Position.X + (int) EndPosition.X,
                (int) EndPosition.Y + (int) Position.Y);
        }
    }
}