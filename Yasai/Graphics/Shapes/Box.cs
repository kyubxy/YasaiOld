using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Yasai.Graphics.Primitives;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;
using Yasai.Graphics.Shaders;

namespace Yasai.Graphics.Shapes
{
    public class Box : Quad
    {
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            var shaderStore = dependencies.Resolve<ShaderStore>();
            
            Shader = shaderStore.GetResource(Shader.SolidShader);
            Shader.Use();
            Shader.SetVector4("colour", new Vector4(Colour.R, Colour.G, Colour.B, Colour.A));
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            Loaded = true;
        }

        public override void Use()
        {
            base.Use();
            Shader.SetVector4("colour", new Vector4(Colour.R/(float)255, Colour.G/(float)255, Colour.B/(float)255, Colour.A/(float)255));
        }
    }
}