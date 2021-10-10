using Xunit;
using Yasai.Resources;

namespace Yasai.Tests.Resources
{
    public class LinkableTest
    {
        [Fact]
        void TestParentChange()
        {
            
        }

        [Fact]
        void TestChildChange()
        {
            
        }
        
        /*
        [Fact]
        void TestBoth()
        {
            Linkable<int> a = new Linkable<int>(5);
            Linkable<int> b = new Linkable<int>();
            
            b.LinkTo(a);

            a.Value = 7;
            Assert.Equal(7, a.Value);
            Assert.Equal(7, b.Value);

            b.Value += 3;
            Assert.Equal(10, b.Value);
            Assert.NotEqual(10, a.Value);

            a.Value = 69;
            Assert.Equal(69, a.Value);
            Assert.Equal(69, b.Value);
        }

        class Node : Linkable<int>
        {
            private int relativeValue;

            public int RelativeValue
            {
                get => relativeValue;
                set
                {
                    relativeValue = value;
                    Value += relativeValue;
                }
            }

            public Node() => Change += i => Value += i;
        }

        [Fact]
        void TestMoreStuff()
        {
            Node parent = new Node();
            Node child = new Node();
            
            child.LinkTo(parent);

            parent.RelativeValue = 10;
            Assert.Equal(10, parent.Value);
            Assert.Equal(10, child.Value);
            
            child.RelativeValue = 
        }*/
    }
}