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
            DependencyContainer container = new DependencyContainer(); 
            var dep = new TestDependency(); 
            container.Register<ITestDependency>(dep); 
            var resolved = container.Resolve<ITestDependency>(); 
            Assert.Same(dep, resolved);
        }
        
         [Fact]
         void testCacheWithName()
         {
             DependencyContainer container = new DependencyContainer();
             var dep1 = new TestDependency();
             var dep2 = new TestDependency();
             container.Register<ITestDependency>(dep1, "1");
             container.Register<ITestDependency>(dep2, "2");
             var resolved1 = container.Resolve<ITestDependency>("1");
             var resolved2 = container.Resolve<ITestDependency>("2");
             Assert.Same(dep1, resolved1);
             Assert.Same(dep2, resolved2);
         }
    }
}