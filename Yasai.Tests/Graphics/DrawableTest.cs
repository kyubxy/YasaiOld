using OpenTK.Mathematics;
using Xunit;
using Yasai.Graphics;

namespace Yasai.Tests.Graphics
{
    public class DrawableTest
    {
        [Fact]
        void testAnchors()
        {
            var tests = new Vector2i[]
            {
                new(-1,  1), // 0
                new( 0,  1), // 1
                new( 1,  1), // 2
                
                new(-1,  0), // 3
                new( 0,  0), // 4
                new( 1,  0), // 5
                
                new(-1, -1), // 6
                new( 0, -1), // 7
                new( 1, -1), // 8
            };
            
            for (int i = 0; i < tests.Length; i++)
                Assert.Equal(tests[i], Drawable.AnchorToUnit(i));
        }
    }
}