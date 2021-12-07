using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public interface ITransform
    {
        /// <summary>
        /// radian measure from vertical
        /// </summary>
        float Rotation { get; }

        /// <summary>
        /// Position
        /// </summary>
        Vector2 Position { get; }
        
        /// <summary>
        /// not Scaling factor
        /// </summary>
        Vector2 Size { get; }
        
        /// <summary>
        /// Anchor relative to parent
        /// </summary>
        Anchor Anchor { get; }
        
        /// <summary>
        /// Anchor relative to sprite
        /// </summary>
        Anchor Origin { get; }
        
        /// <summary>
        /// Origin offset relative to sprite
        /// </summary>
        Vector2 Offset { get; }
        
        /// <summary>
        /// Absolute matrix positioning information
        /// </summary>
        Matrix4 ModelTransforms { get; }
    }
    
}