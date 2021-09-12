using System;
using Yasai.Resources;
using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public class Drawable : IDrawable
    {
        // TODO:
        //private Matrix3 parentTransformations;
        
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Origin { get; set; }
        public virtual Vector2 Size { get; set; } = new Vector2(100);
        public virtual float Rotation { get; set; }
        public virtual bool Visible { get; set; } = true;
        public virtual bool Enabled { get; set; } = true;
        public virtual bool Loaded { get; protected set; }
        
        public virtual float Alpha { get; set; }
        
        public virtual Color4 Colour { get; set; }
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