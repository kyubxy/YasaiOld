namespace Yasai.Input.Keyboard
{
    public interface IKeyHandler : IHandler
    {
        void KeyUp(KeyArgs key);
        void KeyDown(KeyArgs key);
    }
}