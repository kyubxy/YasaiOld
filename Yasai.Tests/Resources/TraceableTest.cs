using Xunit;

namespace Yasai.Tests.Resources
{
    public class TraceableTest
    {
        [Fact]
        void TestChange()
        {
            Tracable<int> num = new Tracable<int>(5);
            bool numChanged = false;
            num.Change += _ => numChanged = true;
            num.Value = 8;
            Assert.True(numChanged);
        }
    }
}