namespace Yasai.Input.Keyboard
{
    public interface IKeyListener : IListener
    {
        public void KeyUp(KeyArgs key);

        public void KeyDown(KeyArgs key);
    }
}