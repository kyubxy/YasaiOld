namespace Yasai.Input.Mouse
{
    public interface IMouseListener : IHierarchical
    {
        void MouseDown (MouseArgs args);
        void MouseUp (MouseArgs args);
        void MouseMotion (MouseArgs args);
    }
}