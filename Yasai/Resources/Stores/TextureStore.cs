using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Yasai.Graphics.Imaging;
using Yasai.Structures.DI;

namespace Yasai.Resources.Stores
{
    public class TextureStore : ContentStore<Texture>
    {
        public TextureStore(DependencyContainer container, string root = "Assets")
            : base(container, root)
        { }

        public override string[] FileTypes => new [] {".png", ".jpg", ".jpeg", ".webp"}; 
        public override IResourceArgs DefaultArgs => new EmptyResourceArgs();

        protected override Texture AcquireResource(string path, IResourceArgs args)
        {
            if (args != null)
                GameBase.YasaiLogger.LogWarning("ImageLoader does not support args");
            
            IntPtr handle = (IntPtr)GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, (int)handle);
            
            Image<Rgba32> image = Image.Load<Rgba32>(path);

            image.Mutate(x => x.Flip(FlipMode.Vertical));

            var pixels = new List<byte>(4 * image.Width * image.Height);

            for (int y = 0; y < image.Height; y++) 
            {
                var row = image.GetPixelRowSpan(y);

                for (int x = 0; x < image.Width; x++) 
                {
                    pixels.Add(row[x].R);
                    pixels.Add(row[x].G);
                    pixels.Add(row[x].B);
                    pixels.Add(row[x].A);
                }
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }
    }
}