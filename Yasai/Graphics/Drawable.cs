using System;
using Yasai.Resources;
using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public class Drawable : IDrawable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }
        
        public bool Enabled { get; set; }
        public virtual void Update()
        {
        }

        public bool Visible { get; set; }
        public virtual void Draw(IntPtr renderer)
        {
        }

        public virtual void Dispose()
        {
        }

        public bool Loaded { get; protected set; }
        public virtual void Load(ContentStore cs)
        {
        }
    }
}