namespace Yasai
{
    public interface IUpdate
    {
        public bool Enabled { get; set; }
        public void Update();
    }
}