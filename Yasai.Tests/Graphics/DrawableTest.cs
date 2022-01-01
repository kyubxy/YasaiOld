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
            var tests = new Vector2[]
            {
                new(0, 0), // 0 topleft
                new(0.5f, 0), // 1 top
                new(1, 0), // 2 topright
                
                new(0,0.5f), // 3 left
                new(0.5f,0.5f), // 4 middle
                new(1,0.5f), // 5 right
                
                new(0, 1), // 6 bottomleft
                new(0.5f, 1), // 7 bottom
                new(1, 1), // 8 bottomright
            };
            
            for (int i = 0; i < tests.Length; i++)
                Assert.Equal(tests[i], Drawable.AnchorToUnit(i));
        }
    }
}