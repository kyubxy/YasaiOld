using Xunit;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.Tests.Resources
{
    public class ContentCacheTest
    {
        [Fact]
        void TestLoadAll()
        {
            ContentCache cache = new ContentCache(new Game(), "Assets/LoadAllTest");
            cache.LoadAll();
            
            var firstLoad = Record.Exception(() => cache.GetResource<Texture>("image"));
            Assert.Null(firstLoad);
            
            var secondLoad = Record.Exception(() => cache.GetResource<Texture>("Subdirectory/image"));
            Assert.Null(secondLoad);
        }
    }
}