using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public abstract class Drawable : IDrawable, IGeometry, IGraphicsModifiable
    {
        public virtual DependencyHandler DependencyHandler { get; set; }

        //public Matrix3x2 Transformation => DependencyHandler.Retrieve<Matrix3x2>().Value; 
        
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Origin { get; set; }
        public virtual Vector2 Size { get; set; } = new Vector2(100);
        public virtual float Rotation { get; set; }
        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public virtual bool Loaded { get; protected set; }
        
        public virtual float Alpha { get; set; }
        public virtual Color Colour { get; set; }

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
        
        public virtual void Load(ContentCache cache)
        {
        }

        public virtual void LoadComplete()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(IntPtr renderer)
        {
        }
    }
}