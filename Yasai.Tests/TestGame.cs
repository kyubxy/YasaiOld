using Yasai.Tests.Scenarios;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        public TestGame()
        {
            ScreenMgr.PushScreen(new TextTest());
        }
    }
}