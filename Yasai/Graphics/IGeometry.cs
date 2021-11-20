using System.Numerics;
using Yasai.Structures.Bindables;

namespace Yasai.Graphics
{
    // geometry system kind of suck right now
    // and only exists to serve primitives
    // will be overhauled following openGL migration
    
    // all new geometry functionality should just be put into IGeometry
    
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
        
        /// <summary>
        /// radian measure from vertical
        /// </summary>
        float Rotation { get; set; }
        
        /// <summary>
        /// Anchor relative to parent
        /// </summary>
        Anchor Anchor { get; set; }
        
        /// <summary>
        /// Anchor relative to sprite
        /// </summary>
        Anchor Origin { get; set; }
        
        /// <summary>
        /// Origin offset relative to sprite
        /// </summary>
        Vector2 Offset { get; set; }
    }
}