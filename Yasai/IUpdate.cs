namespace Yasai
{
    public interface IUpdate
    {
        bool Enabled { get; set; }
        public void Update();
    }
}