using System;
using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    /// <summary>
    /// continually composes an <see cref="ITransform"/> with a parent's <see cref="ITransform"/>
    /// </summary>
    public struct Transform : ITransform
    {
        public Vector2 Position => self.Position + (parent?.Position ?? Vector2.Zero);
        public float Rotation => self.Rotation + (parent?.Rotation ?? 0);
        public Vector2 Size => self.Size;

        public Anchor Anchor => self.Anchor;
        public Anchor Origin => self.Origin;
        public Vector2 Offset => self.Offset;

        private ITransform parent;
        private ITransform self;

        public Transform(ITransform self, ITransform parent)
        {
            this.parent = parent;
            this.self = self;
        }

        // not sure why we're multiplying the anchor by 2 but it works so ¯\_(ツ)_/¯
        public Matrix4 ModelTransforms
            => Matrix4.Identity *
               Matrix4.CreateTranslation(new Vector3(AnchorToUnit(Origin) + Offset)) * // origin
               Matrix4.CreateScale(new Vector3(Size)) * // size
               Matrix4.CreateTranslation(2*new Vector3((parent?.Size ?? new Vector2(1366,768)) * (-AnchorToUnit(Anchor) + Vector2i.One) / 2)) * // anchor
               Matrix4.CreateTranslation(new Vector3(Position)) * // position
               Matrix4.Identity;
        
       public static Vector2i AnchorToUnit(Anchor anchor) => AnchorToUnit((int)anchor);
       public static Vector2i AnchorToUnit(int num) => new (1 - num % 3, 1 - (int)Math.Floor((double)num / 3));
    }
}