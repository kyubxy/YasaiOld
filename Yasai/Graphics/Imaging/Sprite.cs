using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Yasai.Graphics.Primitives;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;
using Yasai.Graphics.Shaders;

namespace Yasai.Graphics.Imaging
{
    public class Sprite : Quad
    {
        private Texture texture;
        
        public Sprite(Texture texture) => this.texture = texture;
        
        public override void Load(DependencyContainer dep)
        {
            base.Load(dep);
            var shaderStore = dep.Resolve<ShaderStore>();
            
            Shader = shaderStore.GetResource(Shader.TextureShader);
            Shader.Use();
            
            var vertexLocation = Shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            
            var texCoordLocation = Shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            
            texture.Use();
            
            Loaded = true;
        }

        public override void Draw()
        {
            base.Draw();
            GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
            Shader.SetVector3("colour", new Vector3(Colour.R/(float)255, Colour.G/(float)255, Colour.B/(float)255));
            Shader.SetFloat("alpha", Alpha);
            texture.Use();
        }
    }
}