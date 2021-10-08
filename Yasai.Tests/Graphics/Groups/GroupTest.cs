using System.Numerics;
using Xunit;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Resources;

namespace Yasai.Tests.Graphics.Groups
{
    public class GroupTest
    {
        class ChildDrawable : Drawable
        {
            public Vector2 Test => DependencyHandler.Retrieve<Vector2>().Value;
        }
        
        [Fact]
        void TestDependencyInjection()
        {
            DependencyHandler dh = new DependencyHandler();
            var v = new Tracable<Vector2>(new Vector2(4,5));
            dh.Store(v);
            
            Group group = new Group();
            ChildDrawable cd;

            group.DependencyHandler = dh;
            group.Add (cd = new ChildDrawable());
            
            Assert.Equal(new Vector2(4,5), cd.Test);

            // check if can handle mutations
            v.Value = new Vector2(7, 8);
            Assert.Equal(new Vector2(7,8), cd.Test);
        }

        [Fact]
        void TestLayeredDependencyInjection()
        {
             DependencyHandler dh = new DependencyHandler();
             var v = new Tracable<Vector2>(new Vector2(4,5));
             dh.Store(v);
             
             Group groupA = new Group();
             Group groupB = new Group();

             ChildDrawable drawable = new ChildDrawable();

             groupA.DependencyHandler = dh;
             
             groupB.Add(drawable);
             groupA.Add(groupB);
             
             Assert.Equal(new Vector2(4,5), drawable.Test);
             
            // check if can handle mutations
            v.Value = new Vector2(7, 8);
            Assert.Equal(new Vector2(7,8), drawable.Test);
        }

        [Fact]
        void TestFail()
        {
            Assert.True(false);
        }
    }
}