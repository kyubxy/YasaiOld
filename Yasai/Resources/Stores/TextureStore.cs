using System;
using System.Collections.Generic;
using System.IO;
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
        public override IResourceArgs DefaultArgs => new EmptyResourceArgs();

        protected override Texture AcquireResource(string path, IResourceArgs args)
        {
            Image<Rgba32> image = Image.Load<Rgba32>(path);
            return new Texture(ImageHelpers.GenerateTexture(image), image.Width, image.Height);
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
                var tex = ImageHelpers.LoadSectionFromTexture(sheet, pair.Value);
                AddResource(tex, pair.Key);
            }
        }
    }
}