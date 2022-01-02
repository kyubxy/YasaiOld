using System;
using ManagedBass;

namespace Yasai.Audio
{
    public class Channel : IChannel
    {
        public AudioStream AudioStream;

        public Channel()
        {
        }

        public Channel(AudioStream stream)
        {
            AudioStream = stream;
        }

        public void Play(bool restart = false)
        {
            Bass.ChannelPlay(AudioStream.Handle, restart);
        }

        public void Pause()
        {
            Bass.ChannelPause(AudioStream.Handle);
        }

        public void Stop()
        {
            Bass.ChannelStop(AudioStream.Handle);
        }

        public long Length => Bass.ChannelGetLength(AudioStream.Handle);

        /// <summary>
        /// Current song position, values in seconds
        /// </summary>
        public double Position
        {
            get => Bass.ChannelBytes2Seconds(AudioStream.Handle, Length);
            set => Bass.ChannelSetPosition(AudioStream.Handle, (long) value);
        }
    }
}