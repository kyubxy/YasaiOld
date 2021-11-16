using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Structures
{
    public class DITest
    {
        interface ITestDependency
        {
        }
        
        class TestDependency : ITestDependency
        {
            public TestDependency()
            { }
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
        
         [Fact]
         void testCacheWithName()
         {
             DependencyCache cache = new DependencyCache();
             var dep1 = new TestDependency();
             var dep2 = new TestDependency();
             cache.Register<ITestDependency>(dep1, "1");
             cache.Register<ITestDependency>(dep2, "2");
             var resolved1 = cache.Resolve<ITestDependency>("1");
             var resolved2 = cache.Resolve<ITestDependency>("2");
             Assert.Same(dep1, resolved1);
             Assert.Same(dep2, resolved2);
         }
    }
}