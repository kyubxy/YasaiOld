using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Yasai.Graphics.Imaging
{
    internal static class ImageHelpers
    {
        internal static Texture LoadSectionFromTexture(Image<Rgba32> sheet, Rectangle area)
        {
            var ret = sheet.Clone(x => 
                    x.Crop(area.ToImageSharp())
                );

            var handle = GenerateTexture(ret);
            return new Texture(handle, (int)area.Width, (int)area.Height);
        }
        
        internal static IntPtr GenerateTexture(Image<Rgba32> image)
        {
            IntPtr handle = (IntPtr)GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, (int)handle);
            
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
            
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return handle;
        }
    }
}