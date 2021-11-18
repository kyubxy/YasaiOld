using System;
using System.Drawing;
using System.Numerics;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Graphics
{
    public abstract class Drawable : IDrawable, IGeometry, IGraphicsModifiable 
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Origin { get; set; }
        public virtual Vector2 Size { get; set; } = new (100);
        public virtual float Rotation { get; set; }
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
    }
}