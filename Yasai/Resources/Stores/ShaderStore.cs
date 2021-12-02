using System;
using System.IO;
using Yasai.Graphics.Shaders;
using Yasai.Structures.DI;

namespace Yasai.Resources.Stores
{
    public class ShaderStore : ContentStore<Shader>
    {
        private readonly string fragPath;
        private readonly string vertPath;
        
        public ShaderStore(DependencyContainer container, string root = "Assets", string frag = "Fragment",
            string vert = "Vertex")
            : base(container, root)
        {
            fragPath = frag;
            vertPath = vert;
        }

        public override string[] FileTypes => new[] { ".sh" };
        public override IResourceArgs DefaultArgs => new EmptyResourceArgs();
        
        protected override Shader AcquireResource(string path, IResourceArgs args)
        {
            string frag = "";
            string vert = "";

            foreach (string line in File.ReadAllLines(path))
            {
                if (line.StartsWith("Fragment:"))
                    frag = line.Remove(0, 9).Trim();
                else if (line.StartsWith("Vertex:"))
                    vert = line.Remove(0, 7).Trim();
            }

            if (String.IsNullOrEmpty(frag) || String.IsNullOrEmpty(vert))
                throw new FileLoadException(
                    $"either the fragment shader or the vertex shader was not provided or was malformed in {path}");

            return new Shader(Path.Combine(Root, fragPath, frag), Path.Combine(Root, vertPath, vert));
        }

        public override Shader GetResource(string res)
        {
            return base.GetResource(res);
        }
    }
}