using OpenTK.Graphics.OpenGL4;
using Yasai.Structures.DI;

namespace Yasai.Graphics
{
    public abstract class Primitive : Drawable
    {
        public abstract float[] Vertices { get; }
        public abstract uint[] Indices { get; }

        public int VertexBufferObject, ElementBufferObject;

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            
            // VertexBufferObject
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
            
            // ElementBufferObject
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);
        }

        public override void Dispose()
        {
            base.Dispose();
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(ElementBufferObject);
        }
    }
}