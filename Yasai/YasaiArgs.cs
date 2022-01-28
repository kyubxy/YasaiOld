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
        bool UseAudioEngine { init; get; }
        
        /// <summary>
        /// Whether the program should process input
        /// </summary>
        bool UseInput { init; get; }
    }
    
    public struct YasaiArgs : IYasaiArgs
    {
        public bool UseAudioEngine { init; get; }
        public bool UseInput { init; get; }
    }
}