using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Structures
{
    public class DITest
    {
        interface ITestDependency
        {
            int Information { get; set; }
        }
        
        class TestDependency : ITestDependency
        {
            public int Information { get; set; }

            public TestDependency()
            { }

            public TestDependency(int information) => Information = information;
        }
        
        [Fact]
        void testCacheAndResolve()
        {
            DependencyCache cache = new DependencyCache();
            var dep = new TestDependency();
            cache.Register<ITestDependency>(dep);
            var resolved = cache.Resolve<ITestDependency>();
            Assert.Same(dep, resolved);
        }
    }
}