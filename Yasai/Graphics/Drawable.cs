using System;
using System.Drawing;
using Yasai.Structures.DI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shaders;

namespace Yasai.Graphics
{
    public class Drawable : IDrawable 
    {
        public Drawable Parent { get; set; }
        
        /// <summary>
        /// Relative to parent
        /// </summary>
        public virtual Vector2 Position { get; set; } = Vector2.Zero;
        
        /// <summary>
        /// Relative to image where the range is from [-1,1]
        /// </summary>
        public virtual Vector2 Offset { get; set; } = Vector2.Zero;
        
        public virtual Anchor Anchor { get; set; } = Anchor.TopLeft;
        public virtual Anchor Origin { get; set; } = Anchor.TopLeft;
        public virtual float Rotation { get; set; } = 0;
        
        public virtual Vector2 Size { get; set; } = new (100);
        public virtual RelativeAxes RelativeAxes { get; set; } = RelativeAxes.None;
        
        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public virtual Shader Shader { get; set; }

        // TODO: use a bindable 
        private float alpha = 1;
        public virtual float Alpha
        {
            get => alpha * (Parent?.Alpha ?? 1);
            set => alpha = value;
        }

        public virtual Color Colour { get; set; } = Color.White;
        
        // TODO: also use a bindable 
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

        // TODO: stop making this virtual
        public virtual Matrix4 ModelTransforms => new Transform(this, Parent).ModelTransforms;
        
        public virtual bool Loaded { get; protected set; }

        public virtual void Load(DependencyContainer dep) 
        { }

        public virtual void Update(FrameEventArgs args)
        { }

        public virtual void Use()
        { }
        
        public virtual void Dispose()
        {
            //Parent?.Dispose();
            Shader?.Dispose();
        }

    }
}