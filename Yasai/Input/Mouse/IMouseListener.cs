namespace Yasai.Input.Mouse
{
    public interface IMouseListener : IListener
    {
        void MouseDown (MouseArgs args);
        void MouseUp (MouseArgs args);
        void MouseMotion (MouseArgs args);
    }
}