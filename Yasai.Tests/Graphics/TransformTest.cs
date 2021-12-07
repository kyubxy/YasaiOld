using OpenTK.Mathematics;
using Xunit;
using Yasai.Graphics;

namespace Yasai.Tests.Graphics
{
    // we can only reliably test numbers here
    // need to set up a scenario in the visual tests later
    // to test things like offset and rotation etc
    public class TransformTest
    {
        private Drawable self => new ()
        {
            Position = new Vector2(2, 3),
            Size = new Vector2(6,9),
            Rotation = 2,
            Anchor = Anchor.TopRight,
            Origin = Anchor.TopLeft,
            Offset = Vector2.Zero
        };

        private Drawable parent => new()
        {
            Position = new Vector2(5, 9),
            Size = new Vector2(4, 3),
            Rotation = 6,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Offset = Vector2.Zero
        };

        private ITransform selfTransform => new Transform(self, parent);
        
        // property tests
        [Fact]
        void testRelativePosition() => Assert.Equal(new Vector2(7, 12), selfTransform.Position);
        
        [Fact]
        void testRelativeRotation() => Assert.Equal(8, selfTransform.Rotation);

        [Fact]
        void testAbsoluteSizing() => Assert.Equal(new Vector2(6,9), selfTransform.Size);
        
        // matrix tests
        [Fact]
        void testAnchorPlacement() => Assert.Equal(new Vector3(9, 12, 0), selfTransform.ModelTransforms.ExtractTranslation());
        
        [Fact]
        void testRelativeSizing()
        {
            for (int i = 1; i <= 10; i++)
            {
                Drawable _self = new()
                {
                    Size = new Vector2((float)1/i),
                    RelativeAxes = RelativeAxes.Both,
                };

                Drawable _parent = new()
                {
                    Size = new Vector2(10),
                };

                ITransform transform = new Transform(_self, _parent);

                var expected = (float)10 / i;
                Assert.Equal(new Vector3(expected, expected, 0), transform.ModelTransforms.ExtractScale());
            }
        }
        
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
                Assert.Equal(tests[i], Transform.AnchorToUnit(i));
        }
    }
}