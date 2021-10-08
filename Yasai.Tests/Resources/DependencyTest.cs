using Xunit;
using Yasai.Resources;

namespace Yasai.Tests.Resources
{
    public class DependencyTest
    {
        class DependencyHolder : IDependencyHolder
        {
            public DependencyHandler DependencyHandler { get; set; }

            public int Num => DependencyHandler.Retrieve<int[]>().Value[0];
            public int Num2 => DependencyHandler.Retrieve<int>().Value;
            public int Num3 => DependencyHandler.Retrieve<int>("notdef").Value;

            public DependencyHolder(DependencyHandler dh) => DependencyHandler = dh;
        }
        
        [Fact]
        void TestDependencyWithMutations()
        {
            var handler = new DependencyHandler();
            var nums = new Tracable<int[]>(new [] { 1 });
            handler.Store(nums);
            
            DependencyHolder dh = new DependencyHolder(handler);
            Assert.Equal(1, dh.Num);

            nums.Value[0] = 90;
            Assert.Equal(90, dh.Num);
        }

        [Fact]
        void TestDependencyWithoutMutations()
        {
             var handler = new DependencyHandler();
             Tracable<int> num = new Tracable<int>(4);
             handler.Store(num);
             
             DependencyHolder dh = new DependencyHolder(handler);
             Assert.Equal(4, dh.Num2);

             num.Value = 7;
             Assert.Equal(7, dh.Num2);           
        }

        [Fact]
        void TestContextedDependency()
        {
             var handler = new DependencyHandler();
             Tracable<int> num = new Tracable<int>(4);
             handler.Store(num, "notdef");
             
             DependencyHolder dh = new DependencyHolder(handler);
             Assert.Equal(4, dh.Num3);

             num.Value = 7;
             Assert.Equal(7, dh.Num3);           
        }
    }
}