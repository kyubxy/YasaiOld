using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Input.Mouse
{
    public interface IMouseListener
    {
        bool IgnoreHierachy { get; }
        
        public void MouseDown (MouseButton button, Vector2 position);
        public void MouseUp (MouseButton button, Vector2 position);
        public void MouseMotion (Vector2 position);
    }
}