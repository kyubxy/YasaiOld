using System;
using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;

namespace Yasai.Graphics.Text
{
    public class Glyph : IGlyphData, IDisposable
    {
        public Vector2i Offset { get; init; }
        public int XAdvance { get; init; }
        public Texture Texture { get; }

        public Glyph(Texture tex) => Texture = tex;

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}