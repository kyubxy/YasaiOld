namespace Yasai.Input.Keyboard
{
    public interface IKeyListener : IListener
    {
        public void KeyUp(KeyCode key);

        public void KeyDown(KeyCode key);
    }
}