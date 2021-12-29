using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
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
            
            Image<Rgba32> image = Image.Load<Rgba32>(path);
            return new Texture(generateTexture(image));
        }
        
        private IntPtr generateTexture(Image<Rgba32> image)
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
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return handle;
        }

        /// <summary>
        /// Add a collection of images to the store through a spritesheet
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadSpritesheet(string sheetLocation, SpritesheetData sheetData)
        {
            Image<Rgba32> sheet = Image.Load<Rgba32>(Path.Combine(Root, sheetLocation));
            
            foreach (KeyValuePair<string, SpritesheetData.Tile> pair in sheetData.SheetData)
                loadSection(sheet, pair.Key, pair.Value);
        }

        private void loadSection(Image<Rgba32> sheet, string name, SpritesheetData.Tile tile)
        {
            var area = tile.Rect;

            var ret = sheet.Clone(x => 
                    x.Crop(area.ToImageSharp())
                );

            var handle = generateTexture(ret);
            Resources[name] = new Texture(handle);
        }
    }
}