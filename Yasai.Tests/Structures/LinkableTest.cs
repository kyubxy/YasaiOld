using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Structures
{
    public class LinkableTest
    {
        /// <summary>
        /// Changing parent value should affect both child and parent
        /// </summary>
      //  [Fact]
        void TestParentChange()
        {
            Linkable<int> parent = new Linkable<int>(3);
            Linkable<int> child = new Linkable<int>();
            child.LinkTo(parent);

            parent.Value = 7;
            
            Assert.Equal(7, parent.Value);
            Assert.Equal(7, child.Value);
        }

        /// <summary>
        /// Changing child value should only affect the child (and not the parent)
        /// </summary>
       // [Fact]
        void TestChildChange()
        {
            Linkable<int> parent = new Linkable<int>(3);
            Linkable<int> child = new Linkable<int>(3);
            child.LinkTo(parent);

            child.Value = 7;

            Assert.Equal(3, parent.Value);
            Assert.Equal(7, child.Value);
        }

        /// <summary>
        /// Mutating child should not also mutate parent
        /// </summary>
       // [Fact]
        void TestChildMutate()
        {
            Linkable<int[]> parent = new Linkable<int[]>(new []{3});
            Linkable<int[]> child = new Linkable<int[]>(new [] {3});
            child.LinkTo(parent);

            Assert.NotSame(parent.Value, child.Value);

            child.Value[0] = 7;

            Assert.Equal(7, child.Value[0]);
            Assert.Equal(3, parent.Value[0]);
            
            Assert.NotSame(parent.Value, child.Value);
        }
    }
}