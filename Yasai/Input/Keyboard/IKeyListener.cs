namespace Yasai.Input.Keyboard
{
    public interface IKeyListener : IHierarchical
    {
        public void KeyUp(KeyCode key);

        public void KeyDown(KeyCode key);
    }
}