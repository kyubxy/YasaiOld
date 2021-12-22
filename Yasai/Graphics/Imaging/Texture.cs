using System;
using OpenTK.Graphics.OpenGL4;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Texture : Resource
    {
        private readonly IntPtr handle;
        
        public Texture(IntPtr handle)
            => this.handle = handle;

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