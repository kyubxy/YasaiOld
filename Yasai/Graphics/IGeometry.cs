using System.Numerics;

namespace Yasai.Graphics
{
    public interface IPoint
    {
        Vector2 Position { get; set; }
    }

    public interface ISimpleGeometry : IPoint
    {
        Vector2 Size { get; set; }
    }

    public interface IGeometry : ISimpleGeometry
    {
        float Rotation { get; set; }
    }
}