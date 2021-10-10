using Xunit;
using Yasai.Resources;
using Yasai.Structures;

namespace Yasai.Tests.Resources
{
    public class LinkableTest
    {
        /// <summary>
        /// Changing parent value should affect both child and parent
        /// </summary>
        [Fact]
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
        [Fact]
        void TestChildChange()
        {
             Linkable<int> parent = new Linkable<int>(3);
             Linkable<int> child = new Linkable<int>(3);
             child.LinkTo(parent);
 
             child.Value = 7;
             
             Assert.Equal(3, parent.Value);
             Assert.Equal(7, child.Value);           
        }
    }
}