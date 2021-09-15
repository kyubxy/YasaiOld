using System;
using OpenTK.Mathematics;

namespace Yasai.Input.Mouse
{
    public class MouseArgs : EventArgs
    {
        public Vector2 Position { get; set; }
        public MouseButton Button { get; set; }

        public MouseArgs(MouseButton m, Vector2 p)
        {
            Position = p;
            Button = m;
        }

        public MouseArgs(Vector2 p) => Position = p;
    }
}