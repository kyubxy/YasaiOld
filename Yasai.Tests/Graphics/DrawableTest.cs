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
                new( 1,  1), // 0 topleft
                new( 0,  1), // 1 top
                new(-1,  1), // 2 topright
                
                new( 1,  0), // 3 left
                new( 0,  0), // 4 middle
                new(-1,  0), // 5 right
                
                new( 1, -1), // 6 bottomleft
                new( 0, -1), // 7 bottom
                new(-1, -1), // 8 bottomright
            };
            
            for (int i = 0; i < tests.Length; i++)
                Assert.Equal(tests[i], Drawable.AnchorToUnit(i));
        }
    }
}