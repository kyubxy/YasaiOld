using System;
using System.Collections.Generic;

namespace Yasai
{
    public interface IYasaiArgs
    {
        /// <summary>
        /// Whether the bass audio engine should be initialised and disposed of
        /// Setting to false will remove all audio
        /// </summary>
        bool EnableAudio { get; }
        
        /// <summary>
        /// Whether the program should process input
        /// </summary>
        bool EnableInput { get; }
    }
    
    public struct YasaiArgs : IYasaiArgs
    {
        public bool EnableAudio { init; get; }
        public bool EnableInput { init; get; }
    }
}