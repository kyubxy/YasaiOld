namespace Yasai.Audio
{
    public interface IChannel
    {
        double Position { get; set; }
        long Length { get; }

        void Play(bool restart);
        void Pause();
        void Stop();
    }
}