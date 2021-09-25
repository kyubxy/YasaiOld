using System;
using Yasai.Platform.OperatingSystems;

namespace Yasai.Platform
{
    public class PlatformManager
    {
        public IPlatform CurrentPlatform;
        
        public PlatformManager()
        {
            // hell yeah !
            if (OperatingSystem.IsWindows())
                CurrentPlatform = new WindowsPlatform();
            else if (OperatingSystem.IsLinux())
                CurrentPlatform = new LinuxPlatform();
            else
                throw new InvalidOperationException(
                    $"could not find a suitable platform for {Environment.OSVersion.Platform.ToString()}");
        }
    }
}