using Yasai.Graphics.Imaging;
using Yasai.Graphics.Layout;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        public TestGame()
        {
            ScreenMgr.PushScreen(new TestScr());
        }
    }

    public class TestScr : Screen
    {
        public TestScr()
        {
            Add(new Sprite());
        }
    }
}