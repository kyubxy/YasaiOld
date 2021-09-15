namespace Yasai.Input.Keyboard
{
    public interface IKeyListener 
    {
        bool IgnoreHierachy { get; }
        public void KeyUp(KeyCode key);

        public void KeyDown(KeyCode key);
    }
}