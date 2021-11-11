using System;
using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Structures
{
    public class BindableTest
    {
        class TestBindable : Bindable<int>
        {
            public TestBindable(int t) : base(t)
            { }
            
            public TestBindable() 
            { }
        }

        [Fact]
        void testUniBind()
        {
            TestBindable a = new TestBindable();
            TestBindable b = new TestBindable(9);

            Assert.Equal(0, a.Value);
            Assert.Equal(9, b.Value);

            a.BindTo(b);
            
            Assert.Equal(9, a.Value);
            Assert.Equal(9, b.Value);

            b.Value = 12;
            
            Assert.Equal(12, a.Value);
            Assert.Equal(12, b.Value);

            Assert.Throws<InvalidOperationException>(() => a.Value = 69);
        }

        [Fact]
        void testBiBind()
        {
             TestBindable a = new TestBindable(3);
             TestBindable b = new TestBindable(9);
             
             Assert.Equal(3, a.Value);
             Assert.Equal(9, b.Value);

             a.Bind(b);
             
             Assert.Equal(9, a.Value);
             Assert.Equal(9, b.Value);

             a.Value = 69;
             
             Assert.Equal(69, a.Value);
             Assert.Equal(69, b.Value);
             
             b.Value = 12;
             
             Assert.Equal(12, a.Value);
             Assert.Equal(12, b.Value);
        }

        [Fact]
        void testUnbind()
        {
            TestBindable a = new TestBindable(3);
            TestBindable b = new TestBindable(9);
            
            Assert.Equal(3, a.Value);
            Assert.Equal(9, b.Value);

            a.BindTo(b);
            
            Assert.Equal(9, a.Value);
            Assert.Equal(9, b.Value);           
 
            b.Value = 12;

            Assert.Equal(12, a.Value);
            Assert.Equal(12, b.Value);
            
            a.Unbind();
            
            b.Value = 69;
            
            Assert.Equal(12, a.Value);
            Assert.Equal(69, b.Value);
        }
    }
}