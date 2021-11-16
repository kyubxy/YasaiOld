using System;
using Xunit;
using Yasai.Structures.Bindables;

namespace Yasai.Tests.Structures.Bindables
{
    public class BindableTypeTest
    {
        [Fact]
        void testBindableInt()
        {
            BindableInt x = new BindableInt(5);
            BindableInt y = new BindableInt();
            
            // uni test

            Assert.Equal(5, x.Value);
            Assert.Equal(0, y.Value);
            
            y.BindTo(x);
            
            Assert.Equal(5, x.Value);
            Assert.Equal(5, y.Value);

            x.Value = 9;
            
            Assert.Equal(9, x.Value);
            Assert.Equal(9, y.Value);

            Assert.Throws<InvalidOperationException>(() => y.Value = 20);
            
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

        [Fact]
        void testBindableVector()
        {
            
        } 
    }
}