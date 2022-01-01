using System;
using OpenTK.Graphics.OpenGL4;

namespace Yasai.Graphics.Imaging
{
    public class Texture : IDisposable
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

        public void Dispose()
        {
            GL.DeleteTexture((int)handle);
        }
    }
}