namespace Yasai.Graphics.Text
{
    public interface IFontData
    {
        bool Bold { get; init; }
        bool Italic { get; init; }
        int Size { get; init; }
    }
}