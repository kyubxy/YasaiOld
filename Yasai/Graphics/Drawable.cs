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

        public virtual Vector2 Size { get; set; } = new(100);
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

        private ITransform parentTransform => Parent?.AbsoluteTransform ?? new Transform(
            position: Vector2.Zero,
            size: new Vector2(100),
            rotation: 0,
            anchor: Anchor.TopLeft,
            origin: Anchor.TopLeft,
            offset: Vector2.Zero
        );

        private Vector2 origin => Size * (AnchorToUnit(Origin) + Offset);
        private Vector2 pos => Position + parentTransform.Position;
        private Vector2 anchor => parentTransform.Size * AnchorToUnit(Anchor);

        public Transform AbsoluteTransform => new(
            position: pos - origin + anchor,
            size: Size,
            rotation: Rotation + parentTransform.Rotation,
            anchor: Anchor,
            origin: Origin,
            offset: Offset
        );

        private Vector2 pivot => Parent == null ? origin : parentTransform.Position + origin;

        // how to actually draw
        public Matrix4 ModelTransforms =>
            // rotation
            Matrix4.CreateTranslation(new Vector3(1,1,0)) *
            
            Matrix4.CreateRotationZ(AbsoluteTransform.Rotation) *
            
            // scale
            Matrix4.CreateScale(new Vector3(AbsoluteTransform.Size / 2)) * 
            
            // position
            Matrix4.CreateTranslation(new Vector3(AbsoluteTransform.Position)) *
            Matrix4.Identity;

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
            //Shader?.Dispose();
        }
        
        public static Vector2 AnchorToUnit(Anchor anchor) => AnchorToUnit((int)anchor);
        public static Vector2 AnchorToUnit(int num) => new ((float)num % 3 / 2, (float)Math.Floor((double)num / 3) / 2);
    }
}