namespace Yasai.Input
{
    public interface IKeyListener 
    {
        bool IgnoreHierachy { get; }
        public void KeyUp(KeyCode key);

        public void KeyDown(KeyCode key);
    }
}