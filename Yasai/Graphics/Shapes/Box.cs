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
            Shader.SetVector4("colour", new Vector4(1,1,1,1));
        }
    }
}