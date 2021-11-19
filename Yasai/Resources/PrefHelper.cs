using System;
using System.IO;
using System.Reflection;

namespace Yasai.Resources
{
    public static class PrefHelper
    {
        /// <summary>
        /// Home directory for save files
        /// </summary>
        public static string HomeDirectory 
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();

                if (assembly == null)
                    throw new Exception("could not find an entry assembly");

                var dir = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), 
                    assembly.GetName().Name ?? "UnknownYasaiApplication");

                // if the directory doesn't already exist, create it
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                return dir;
            }
        }
    }
}