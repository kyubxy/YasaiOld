using System;
using System.IO;
using Yasai.Graphics.Shaders;
using Yasai.Structures.DI;

namespace Yasai.Resources.Stores
{
    public class ShaderStore : ContentStore<Shader>
    {
        public ShaderStore(DependencyContainer container, string root = "Assets") 
            : base(container, root)
        { }

        public override string[] FileTypes => new[] { ".sh" };
        public override IResourceArgs DefaultArgs => new ShaderArgs("Fragment", "Vertex");
        
        protected override Shader AcquireResource(string path, IResourceArgs args)
        {
            string frag = "";
            string vert = "";

            var sargs = args as ShaderArgs;
            var fragPath = sargs == null ? ((ShaderArgs)DefaultArgs).FragmentPath : sargs.FragmentPath;
            var vertPath = sargs == null ? ((ShaderArgs)DefaultArgs).VertexPath : sargs.VertexPath;
            
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
    }

    public class ShaderArgs : IResourceArgs
    {
        public string FragmentPath { get; }
        public string VertexPath { get; }

        public ShaderArgs(string frag, string vert)
        {
            FragmentPath = frag;
            VertexPath = vert;
        }
    }
}