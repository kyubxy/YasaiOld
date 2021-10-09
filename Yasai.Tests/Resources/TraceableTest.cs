using Xunit;

namespace Yasai.Tests.Resources
{
    public class TraceableTest
    {
        [Fact]
        void TestChange()
        {
            Traceable<int> num = new Traceable<int>(5);
            bool numChanged = false;
            num.Change += _ => numChanged = true;
            num.Value = 8;
            Assert.True(numChanged);
        }

        /*
        [Fact]
        void TestDuplication()
        {
            bool numChanged = false;
            Traceable<int> num = new Traceable<int>(5);
            num.Change += _ => numChanged = true;
            Traceable<int> anotherNum = (Traceable<int>) num.Clone();

            // numChanged is false
            Assert.False(numChanged);
            // traceables are value equivalent
            Assert.Equal(num.Value, anotherNum.Value);
            // traceables are not the same instance
            Assert.NotSame(num, anotherNum);
            
            // test only changing num triggers anotherNum
            
            int anotherNumChanged = 0;
            anotherNum.Change += _ => anotherNumChanged++;
            // anotherNumChanged actually works
            anotherNum.Value = 5;
            Assert.Equal(1, anotherNumChanged);

            num.Value = 6;
            // num has changed
            Assert.True(numChanged);
            // anotherNum has changed
            Assert.Equal(2, anotherNumChanged);
        }
        */
    }
}