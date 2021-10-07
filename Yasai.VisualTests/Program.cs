using System;

namespace Yasai.VisualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new TestGame())
                game.Run();
        }
    }
}
