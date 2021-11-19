namespace Yasai.Input.Mouse
{
    public interface IMouseHandler : IHandler
    {
        void MouseDown (MouseArgs args);
        void MouseUp (MouseArgs args);
        void MouseMotion (MouseArgs args);
    }
}