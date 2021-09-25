using System;
using System.Numerics;

namespace Yasai.Input.Mouse
{
    public class MouseArgs : EventArgs
    {
        public Vector2 Position { get; }
        public MouseButton Button { get; }

        public MouseArgs(MouseButton m, Vector2 p)
        {
            Position = p;
            Button = m;
        }

        public MouseArgs(Vector2 p) => Position = p;
    }
}