using System;

namespace Yasai.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Root root = new Root ())
                root.Run();
        }
    }
}
