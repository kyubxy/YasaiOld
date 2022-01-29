using System;
using Xunit;
using Yasai.Structures;
using Yasai.Structures.Bindables;

namespace Yasai.Tests.Structures.Bindables
{
    public class BindableTypeTest
    {
        //[Fact]
        /*
        void testBindStringToInt()
        {
            BindableString str = new BindableString();
            BindableInt n = new BindableInt(4);

            str.BindTo(n);

            Assert.Equal("4", str.Value);

            n.Value = 69;
            
            Assert.Equal("69", str.Value);
        }
        */
        
        /*
        [Fact]
        void testBindableInt()
        {
            Bindable<int> x = new Bindable<int>(5);
            Bindable<int> y = new Bindable<int>();
            
            // uni test

            Assert.Equal(5, x.Value);
            Assert.Equal(0, y.Value);
            
            y.BindTo(x);
            
            Assert.Equal(5, x.Value);
            Assert.Equal(5, y.Value);

            x.Value = 9;
            
            Assert.Equal(9, x.Value);
            Assert.Equal(9, y.Value);

            // despite the throws call, sometimes xunit doesn't
            // block the exception halting testing
            try
            {
                Assert.Throws<InvalidOperationException>(() => y.Value = 20);
            }
            catch (InvalidOperationException e)
            { }
            
            y.Unbind();

            x.Value = 56;
            y.Value = 57;
            
            Assert.Equal(56, x.Value);
            Assert.Equal(57, y.Value);
            
            // bi test
            
            x.Bind(y);

            x.Value = 90;
            
            Assert.Equal(90, x.Value);
            Assert.Equal(90, y.Value);
            
            y.Value = 200;
            
            Assert.Equal(200, x.Value);
            Assert.Equal(200, y.Value);
        }
        */
    }
}