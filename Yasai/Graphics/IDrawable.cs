using System;
using System.Drawing;
using OpenTK.Mathematics;
using Yasai.Graphics.Shaders;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IDisposable //IMouseHandler, IKeyHandler
    {
        /// <summary>
        /// radian measure from vertical
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Opacity, ranges between 0 and 1
        /// </summary>
        float Alpha { get; set; }
        
        /// <summary>
        /// Tint
        /// </summary>
        Color Colour { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        Vector2 Position { get; set; }
        
        /// <summary>
        /// Sizing
        /// </summary>
        Vector2 Size { get; set; }
        
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
        
        /// <summary>
        /// Parent
        /// </summary>
        IDrawable Parent { get; set; }
        
        Matrix4 ModelTransforms { get; }
        
        /// <summary>
        /// Whether to draw or not
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// shader
        /// </summary>
        Shader Shader { get; set; } // <- currently tight coupling to shader
    }
}