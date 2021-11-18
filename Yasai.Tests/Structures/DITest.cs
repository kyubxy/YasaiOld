using Xunit;
using Xunit.Abstractions;
using Yasai.Structures;
using Yasai.Structures.DI;

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

        class TestTransientDependency : ITestDependency, ITransientDependency<TestTransientDependency>
        {
            public TestTransientDependency GetNewService()
                => new ();
        }

        [Fact]
        void testSingletonRegisterAndResolve()
        {
            DependencyContainer container = new DependencyContainer(); 
            var dep = new TestDependency(); 
            container.Register<ITestDependency>(dep); 
            var resolved = container.Resolve<ITestDependency>(); 
            Assert.Same(dep, resolved);
        }
        
         [Fact]
         void testSingleRegisterAndResolveWithName()
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

         [Fact]
         void testTransientRegisterAndResolve()
         {
             DependencyContainer container = new DependencyContainer();
             var dep = new TestTransientDependency();
             container.RegisterTransient<ITestDependency, TestTransientDependency>(dep);
             var resolved = container.Resolve<ITestDependency>();
             Assert.NotSame(dep, resolved);
         }
    }
}