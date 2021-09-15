using System;
using OpenTK.Mathematics;
using Yasai.Input.Mouse;

namespace Yasai.Graphics.Layout.Groups
{
    public class ClickableGroup : Group
    {
        public event EventHandler OnClick;
        public event EventHandler OnRelease;
        public event EventHandler OnHover;
        
        public override void MouseDown(MouseArgs args)
        {
            base.MouseDown(args);
            if (Hovering (args.Position))
            {
                EventHandler handler = OnClick;
                handler?.Invoke(this, args);
            }
        }
        
        public override void MouseUp(MouseArgs args)
        {
            base.MouseUp(args);
            if (Hovering (args.Position))
            {
                EventHandler handler = OnRelease;
                handler?.Invoke(this, args);
            }
        }

        public override void MouseMotion(MouseArgs args)
        {
            base.MouseMotion(args);
            if (Hovering (args.Position))
            {
                EventHandler handler = OnHover;
                handler?.Invoke(this, args);
            }
        }

        protected virtual bool Hovering(Vector2 position)
            => position.X > Position.X && position.X < Position.X + Size.X && position.Y > Position.Y &&
               position.Y < Position.Y + Size.Y;
    }
}