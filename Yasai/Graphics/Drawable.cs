using System;
using System.Drawing;
using System.Numerics;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Maths;
using Yasai.Structures;
using Yasai.Structures.Bindables;
using Yasai.Structures.DI;

namespace Yasai.Graphics
{
    public abstract class Drawable : IDrawable, IGeometry, IGraphicsModifiable 
    {
        public virtual Vector2 Position { get; set; } = Vector2.Zero;
        public virtual Vector2 Size { get; set; } = new (100);
        public virtual float Rotation { get; set; } 
        public virtual Anchor Anchor { get; set; } = Anchor.TopLeft;
        public virtual Anchor Origin { get; set; } = Anchor.TopLeft;
        public virtual Vector2 Offset { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;

        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        
        protected DependencyContainer Dependencies { get; set; }
        public virtual bool Loaded => Dependencies != null;

        public Drawable Parent { get; set; }

        private float alpha = 1;
        public virtual float Alpha
        {
            get => alpha * (Parent?.alpha ?? 1);
            set => alpha = value;
        }

        public virtual Color Colour { get; set; }

        public Matrix3 Transformations => Matrix.DotMultiply(Parent?.Transformations ?? Matrix.Identity,
            Matrix.GetTranslationMat(Position));

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