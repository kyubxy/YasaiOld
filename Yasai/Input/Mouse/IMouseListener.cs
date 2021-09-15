using SDL2;

namespace Yasai.Input.Mouse
{
    public interface IMouseListener
    {
        bool IgnoreHierachy { get; }
        
        public void MouseDown (MouseButton button);
        public void MouseUp (MouseButton button);
    }
}