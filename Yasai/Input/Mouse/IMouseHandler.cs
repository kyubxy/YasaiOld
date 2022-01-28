using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Yasai.Input.Mouse
{
    public interface IMouseHandler 
    {
        // false to block, true to not block
        
        // TODO: need a better way of optimising input
        bool EnableGlobalMousePress { get; set; }
        bool EnableGlobalMouseMove { get; set; }
        bool EnableMousePress { get; set; }
        bool EnableMouseMove { get; set; }
        bool EnableMouseScroll { get; set; }
        
        /// <summary>
        /// When the user clicks the mouse, fires once, waits and then fires continually
        /// </summary>
        /// <param name="position"></param>
        /// <param name="buttonArgs"></param>
        void GlobalMousePress(Vector2 position, MouseButtonEventArgs buttonArgs);
        
        /// <summary>
        /// When the user moves the mouse
        /// </summary>
        /// <param name="args"></param>
        void GlobalMouseMove(MouseMoveEventArgs args);
        
        /// <summary>
        /// When the user clicks the mouse, fires once, waits and then fires continually
        /// </summary>
        /// <param name="position"></param>
        /// <param name="buttonArgs"></param>
        /// <returns></returns>
        bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs);
        
        /// <summary>
        /// When the user moves the mouse
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool MouseMove(MouseMoveEventArgs args);
        
        /// <summary>
        /// When the user scrolls on the mouse
        /// </summary>
        /// <param name="position"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool MouseScroll(Vector2 position, MouseWheelEventArgs args);
        
        event Action<Vector2, MouseButtonEventArgs> MouseClickEvent;
        event Action<Vector2, MouseButtonEventArgs> MousePressEvent;
        event Action<MouseMoveEventArgs> MouseMoveEvent;
        event Action MouseEnterEvent;
        event Action MouseExitEvent;
        event Action<Vector2, MouseWheelEventArgs> MouseScrollEvent;
    }
}