namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate
    {
        public bool Visible { get; set; }
        public void Draw();
    }
}