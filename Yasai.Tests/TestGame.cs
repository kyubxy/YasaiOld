using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        public TestGame()
        {
            Children.Add(new Sprite ("ino.png")
            {
                Position = Vector2.One,
                Size = new Vector2 (20)
            });
        }
    }
}