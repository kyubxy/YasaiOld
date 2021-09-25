using Yasai.Platform.OperatingSystems;

namespace Yasai.Platform
{
    public interface IPlatform
    {
        OS OperatingSystem { get; }
        
        /// <summary>
        /// Initialise relevant external dependencies to access SDL engine
        /// </summary>
        void InitialiseSdlSystems();
    }
}