using System.Runtime.Intrinsics.X86;
using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        private Sprite s;
        public TestGame()
        {
            Children.Add(s = new Sprite ("ino")
            {
                Position = Vector2.One,
                Size = new Vector2 (300)
            });
        }

        public override void Load()
        {
            Content.LoadResource("ino.png");
            base.Load();
        }

        public override void Update()
        {
            s.Position = new Vector2(s.Position.X + 0.115f, s.Position.Y + 0.111f);
            base.Update();
        }
    }
}