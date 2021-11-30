using Yasai.Graphics.Primitives;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Shapes
{
    public class Box : Quad
    {
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            var shaderStore = dependencies.Resolve<ShaderStore>();
            Shader = shaderStore.GetResource("solid");
        }
    }
}