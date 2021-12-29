using System;
using OpenTK.Graphics.OpenGL4;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Texture : Resource
    {
        private readonly IntPtr handle;

        public int Width { get; }
        public int Height { get; }

        public Texture(IntPtr handle, int w, int h)
        {
            this.handle = handle;
            Width = w;
            Height = h;
        }

        // TODO: should Texture0 be specifiable as a parameter
        /// <summary>
        /// Sets the active texture and binds it 
        /// </summary>
        public void Use()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, (int)handle);
        }
    }
}