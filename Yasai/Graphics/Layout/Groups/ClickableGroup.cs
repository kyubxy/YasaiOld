using System;
using System.Numerics;

using Yasai.Input.Mouse;

namespace Yasai.Graphics.Layout.Groups
{
    public class ClickableGroup : Group
    {
        public event EventHandler OnClick;
        public event EventHandler OnRelease;
        public event EventHandler OnHover;
        public event EventHandler OnEnter;
        public event EventHandler OnExit;

        private bool _mouseDownInside;

        public override bool Enabled { get; set; } = true;

        public override void MouseDown(MouseArgs args)
        {
            base.MouseDown(args);
            if (Hovering (args.Position))
            {
                _mouseDownInside = true;
                EventHandler handler = OnClick;
                handler?.Invoke(this, args);
            }
        }
        
        public override void MouseUp(MouseArgs args)
        {
            base.MouseUp(args);
            if (Hovering (args.Position) && _mouseDownInside)
            {
                EventHandler handler = OnRelease;
                handler?.Invoke(this, args);
            }

            _mouseDownInside = false;
        }

        private bool previousHover;
        public override void MouseMotion(MouseArgs args)
        {
            base.MouseMotion(args);
            bool currentHover = Hovering(args.Position);
            if (currentHover)
            {
                EventHandler handler = OnHover;
                handler?.Invoke(this, args);
            }

            if (currentHover && !previousHover)
            {
                EventHandler handler = OnEnter;
                handler?.Invoke(this, args);
            } 
            else if (!currentHover && previousHover)
            {
                EventHandler handler = OnExit;
                handler?.Invoke(this, args);
            }

            previousHover = Hovering(args.Position);
        }

        protected virtual bool Hovering(Vector2 position)
            => position.X > Position.X && position.X < Position.X + Size.X && position.Y > Position.Y &&
               position.Y < Position.Y + Size.Y;
    }
}