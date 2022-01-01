using System;
using ManagedBass;

namespace Yasai.Audio
{
    /// <summary>
    /// Sound data
    /// </summary>
    public class AudioStream : IDisposable
    {
        public int Handle { get; }

        public AudioStream(int handle)
        {
            Handle = handle;
        }
        
        public void Dispose()
        {
            Bass.StreamFree(Handle);
        }
    }
}