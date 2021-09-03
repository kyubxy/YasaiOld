using System;
using Yasai.Resources;
using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public class Drawable : IDrawable
    {
        // TODO:
        //private Matrix3 parentTransformations;
        
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Loaded { get; protected set; }
        public virtual void Load(ContentStore cs)
        {
        }
        public virtual void Update()
        {
        }

        public virtual void Draw(IntPtr renderer)
        {
        }

        public virtual void Dispose()
        {
        }

    }
}