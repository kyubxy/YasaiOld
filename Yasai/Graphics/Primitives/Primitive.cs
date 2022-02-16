using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Primitives
{
    public abstract class Primitive : Drawable
    {
        protected abstract float[] Vertices { get; }
        public abstract uint[] Indices { get; }

        private int vertexBufferObject, elementBufferObject;

        public override void Load(DependencyContainer container)
        {
            base.Load(container);
            
            // VertexBufferObject
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
            
            // ElementBufferObject
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);
        }

        public override void Draw()
        {
            base.Draw();
            
            // this has truly turned into a true shitshow
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);
        }

        public override void Dispose()
        {
            base.Dispose();
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteBuffer(elementBufferObject);
        }
    }
}