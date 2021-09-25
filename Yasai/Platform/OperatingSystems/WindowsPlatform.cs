using System;
using System.IO;

namespace Yasai.Platform.OperatingSystems
{
    public class WindowsPlatform : IPlatform
    {
        public OS OperatingSystem => OS.Windows;

        public void InitialiseSdlSystems()
        {
            // no extra steps need to be taken for the windows runtime
            // just check if dependencies are in the root directory
            
            string[] dependencies = 
            {
                "SDL2.dll",
                "SDL2_image.dll",
                "SDL2_ttf.dll",
            };

            foreach (string d in dependencies)
            {
                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), d)))
                    throw new DllNotFoundException(
                        $"{d} was not present in the root folder, ensure you have the necessary dependencies before proceeding");
            }
        }
    }
}