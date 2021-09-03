using System;
using Yasai.Graphics.Imaging;

namespace Yasai.Resources.Loaders
{
    public class ImageLoader : ILoader <Texture>
    {
        public string[] FileTypes => new string[] {"png", "jpg", "jpeg", "PNG", "JPEG"};
        public Texture GetResource(string path)
        {
            throw new NotImplementedException();
        }
    }
}