using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using Yasai.Structures.DI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shaders;
using Vector2 = OpenTK.Mathematics.Vector2;
using Vector3 = OpenTK.Mathematics.Vector3;

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

        // everything works except rotation
        private Vector2 pos => Position + parentTransform.Position;
        private Vector2 anchor => parentTransform.Size * AnchorToUnit(Anchor);
        private Vector2 origin => Size * (AnchorToUnit(Origin) + Offset);

        public Transform AbsoluteTransform => new(
            position: pos - origin + anchor,
            size: Size,
            rotation: Rotation + parentTransform.Rotation,
            anchor: Anchor,
            origin: Origin,
            offset: Offset
        );

        Vector2 sizing
        {
            get
            {
                var originalSize = AbsoluteTransform.Size / 2;

                Vector2 ret;
                
                switch (RelativeAxes)
                {
                    case RelativeAxes.None:
                        ret = originalSize;
                        break;
                    case RelativeAxes.Both:
                        ret = parentTransform.Size * originalSize;
                        break;
                    case RelativeAxes.X:
                        ret = new Vector2(parentTransform.Size.X * originalSize.X, originalSize.Y);
                        break;
                    case RelativeAxes.Y:
                        ret = new Vector2(originalSize.X, parentTransform.Size.Y * originalSize.Y);
                        break;
                    default:
                        throw new SwitchExpressionException(); 
                }

                return ret;
            }
        }

        // TODO: make this not suck
        // how to actually draw
        // i have no idea what im doing
        public Matrix4 ModelTransforms =>
            // origin
            Matrix4.CreateTranslation(-new Vector3(AnchorToUnit(Origin)) * 2 + new Vector3(1)) *
            
            // rotation
            Matrix4.CreateRotationZ(AbsoluteTransform.Rotation) *
            
            // undo origin
            Matrix4.Invert(Matrix4.CreateTranslation(-new Vector3(AnchorToUnit(Origin)) * 2 + new Vector3(1))) *
            
            // move back to top left
            Matrix4.CreateTranslation(1, 1, 0) *
            
            // scale
            Matrix4.CreateScale(new Vector3(sizing)) *
            
            // position
            Matrix4.CreateTranslation(new Vector3(AbsoluteTransform.Position)) *
            Matrix4.Identity;

        public virtual bool Loaded { get; protected set; }

        public virtual void Load(DependencyContainer dep) 
        { }

        public virtual void Update(FrameEventArgs args)
        { }

        public virtual void Draw()
        { }
        
        public virtual void Dispose()
        {
            //Parent?.Dispose();
            //Shader?.Dispose();
        }
        
        public static Vector2 AnchorToUnit(Anchor anchor) => AnchorToUnit((int)anchor);
        public static Vector2 AnchorToUnit(int num) => new ((float)num % 3 / 2, (float)Math.Floor((double)num / 3) / 2);

        #region input
        public virtual bool MouseClick(Vector2 position, MouseButtonEventArgs buttonArgs) => true;
        public virtual bool MouseMove(MouseMoveEventArgs args) => true;
        public virtual bool MouseEnter() => true;
        public virtual bool MouseExit() => true;
        public virtual bool MouseScroll(Vector2 position, MouseWheelEventArgs args) => true;
        
        public virtual void KeyDown(KeyboardKeyEventArgs args)
        { }
        
        public virtual void KeyUp(KeyboardKeyEventArgs args)
        { }
        #endregion
    }
}