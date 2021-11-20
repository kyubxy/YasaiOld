using System;
using System.Drawing;
using System.Numerics;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Structures.DI;

namespace Yasai.Graphics
{
    public abstract class Drawable : IDrawable, IGeometry, IGraphicsModifiable 
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Size { get; set; } = new (100);
        public virtual float Rotation { get; set; }
        public virtual Anchor Anchor { get; set; }
        public virtual Anchor Origin { get; set; }
        public virtual Vector2 Offset { get; set; }
        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public virtual bool Loaded => Dependencies != null;

        public virtual float Alpha { get; set; }
        public virtual Color Colour { get; set; }

        protected DependencyContainer Dependencies { get; set; }

        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }
        
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }

        public float Width
        {
            get => Size.X;
            set => Size = new Vector2(value, Size.Y);
        }
        public float Height
        {
            get => Size.Y;
            set => Size = new Vector2(Size.X, value);
        }
        
        public virtual void Load(DependencyContainer dependencies)
            => Dependencies = dependencies;

        public virtual void Update()
        {
        }

        public virtual void Draw(IntPtr renderer)
        {
        }

        # region input
        
        // keyboard
        public virtual void KeyUp(KeyArgs key) 
        { }

        public virtual void KeyDown(KeyArgs key) 
        { }
         
        // mouse
        public event EventHandler OnClick;
        public event EventHandler OnRelease;
        public event EventHandler OnHover;
        public event EventHandler OnEnter;
        public event EventHandler OnExit;

        private bool mouseDownInside;
        public virtual void MouseDown(MouseArgs args)
        {
            if (Hovering (args.Position))
            {
                mouseDownInside = true;
                EventHandler handler = OnClick;
                handler?.Invoke(this, args);
            }
        }
        
        public virtual void MouseUp(MouseArgs args)
        {
            if (Hovering (args.Position) && mouseDownInside)
            {
                EventHandler handler = OnRelease;
                handler?.Invoke(this, args);
            }

            mouseDownInside = false;
        }

        private bool previousHover;
        public virtual void MouseMotion(MouseArgs args)
        {
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
        
        # endregion
    }
}