using OpenTK.Mathematics;
using SDL2;

namespace Yasai.Input.Mouse
{
    public interface IMouseListener
    {
        bool IgnoreHierachy { get; }
        
        public void MouseDown (MouseArgs args);
        public void MouseUp (MouseArgs args);
        public void MouseMotion (MouseArgs args);
    }
}