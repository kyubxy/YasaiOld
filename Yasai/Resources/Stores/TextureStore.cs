using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Yasai.Graphics.Imaging;
using Rectangle = Yasai.Graphics.Rectangle;

namespace Yasai.Resources.Stores
{
    public class TextureStore : Store<Texture>
    {
        public TextureStore(string root = "Assets")
            : base(root)
        { }

        public override string[] FileTypes => new [] {".png", ".jpg", ".jpeg", ".webp"};
        public override IResourceArgs DefaultArgs => new TextureArgs(TextureMinFilter.Linear, TextureMagFilter.Linear);

        protected override Texture AcquireResource(string path, IResourceArgs args)
        {
            TextureArgs targs = (TextureArgs)args;
            
            Image<Rgba32> image = Image.Load<Rgba32>(path);
            return new Texture(ImageHelpers.GenerateTexture(image, targs.MinFilter, targs.MagFilter), image.Width, image.Height);
        }

        /// <summary>
        /// Add a collection of images to the store through a spritesheet
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadSpritesheet(string sheetLocation, Dictionary<string, Rectangle> spritesheetData)
        {
            Image<Rgba32> sheet = Image.Load<Rgba32>(Path.Combine(Root, sheetLocation));

            foreach (KeyValuePair<string, Rectangle> pair in spritesheetData)
            {
                // TODO: might need to pick another filter or expose this 
                var tex = ImageHelpers.LoadSectionFromTexture(sheet, pair.Value, TextureMinFilter.Linear, TextureMagFilter.Linear);
                AddResource(tex, pair.Key);
            }
        }
    }

    public class TextureArgs : IResourceArgs
    {
        public TextureMinFilter MinFilter;
        public TextureMagFilter MagFilter;

        public TextureArgs(TextureMinFilter min, TextureMagFilter mag)
        {
            MinFilter = min;
            MagFilter = mag;
        }
    }
}