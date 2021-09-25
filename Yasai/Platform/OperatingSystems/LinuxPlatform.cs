using System;

namespace Yasai.Platform.OperatingSystems
{
    public class LinuxPlatform : IPlatform
    {
        public OS OperatingSystem => OS.Linux;
        private readonly string _libPath;

        public LinuxPlatform() : this("/usr/lib")
        { }

        public LinuxPlatform(string libPath) => _libPath = libPath;

        public void InitialiseSdlSystems()
        {
            
        }
    }
}