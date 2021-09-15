using System;
using OpenTK.Mathematics;
using Yasai.Input.Mouse;

namespace Yasai.Graphics.Layout.Groups
{
    public class ClickableGroup : Group
    {
        public event EventHandler OnClick;
        public event EventHandler OnRelease;
        
        public override void MouseDown(MouseArgs args)
        {
            base.MouseDown(args);
            if (hovering (args.Position))
            {
                EventHandler handler = OnClick;
                handler?.Invoke(this, args);
            }
        }
        
        public override void MouseUp(MouseArgs args)
        {
            base.MouseUp(args);
            if (hovering (args.Position))
            {
                EventHandler handler = OnRelease;
                handler?.Invoke(this, args);
            }
        }

        // TODO: remove
        private bool hovering(Vector2 position)
            => position.X > Position.X && position.X < Position.X + Size.X && position.Y > Position.Y &&
               position.Y < Position.Y + Size.Y;
    }
}