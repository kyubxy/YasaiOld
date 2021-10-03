using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class ResourceTest : Scenario
    {
        private ContentCache manager;
        
        public ResourceTest(Game game) 
            : base (game)
        {
            manager = new ContentCache(Game);
        }
    }
}