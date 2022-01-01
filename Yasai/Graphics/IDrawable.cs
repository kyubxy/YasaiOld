using System;
using System.Drawing;
using OpenTK.Mathematics;
using Yasai.Graphics.Shaders;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IDisposable, ITransform, IMouseHandler, IKeyboardHandler
    {
        /// <summary>
        /// Opacity, ranges between 0 and 1
        /// </summary>
        float Alpha { get; set; }
        
        /// <summary>
        /// Tint
        /// </summary>
        Color Colour { get; set; }
        
        /// <summary>
        /// Whether to draw or not
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// shader
        /// </summary>
        Shader Shader { get; set; } // <- currently tight coupling to shader
        
        /// <summary>
        /// Parent
        /// </summary>
        Drawable Parent { get; set; }
        
        /// <summary>
        /// Used in propagating the scenegraph
        /// </summary>
        Transform AbsoluteTransform { get; }
        
        /// <summary>
        /// Absolute matrix positioning information
        /// </summary>
        Matrix4 ModelTransforms { get; }

        void Draw();
    }
}