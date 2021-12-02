using System;
using System.Drawing;
using Yasai.Structures.DI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shaders;

namespace Yasai.Graphics
{
    public abstract class Drawable : IDrawable 
    {
        public IDrawable Parent { get; set; }
        
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
        public virtual float Rotation { get; set; }
        
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

        // mainly for OpenGL stuff, thus the 4x4 matrix storing 2D affine transformations in 3D space
        public Matrix4 ModelTransforms 
            => Matrix4.Identity *
               Matrix4.CreateTranslation(-Offset.X - AnchorToUnit(Origin).X, Offset.Y + AnchorToUnit(Origin).Y, 0) * // Origin
               Matrix4.CreateScale(Width, Height, 0f) * // Scale
               Matrix4.CreateRotationZ (Rotation) * // Rotation
               Matrix4.CreateTranslation(X, Y, 0)   // Translation
               ;
                                          
        /*
            => (Parent?.ModelTransforms ?? Matrix.Identity) *
               Matrix.GetTranslationMat(Position) *
               Matrix.GetRotationMat(Rotation) *
               Matrix.GetScaleMat(Scale)
               ;
               */

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

        public static Vector2i AnchorToUnit(Anchor anchor) => AnchorToUnit((int)anchor);
        public static Vector2i AnchorToUnit(int num) => new (num % 3 - 1, 1 - (int)Math.Floor((double)num / 3));
    }
}