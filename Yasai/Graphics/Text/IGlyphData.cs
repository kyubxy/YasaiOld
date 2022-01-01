using OpenTK.Mathematics;

namespace Yasai.Graphics.Text
{
    public interface IGlyphData
    {
        Vector2i Offset { get; init; }
        int XAdvance { get; init; }
    }
}