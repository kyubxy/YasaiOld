using System;
using System.Numerics;
using Xunit;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Resources;
using Yasai.Structures;

namespace Yasai.Tests.Graphics.Groups
{
    public class GroupTest
    {
        class TestDrawable : Drawable
        {
            public Vector2 Test => DependencyCache.Retrieve<Vector2>().Value;
            public int Test2 => DependencyCache.Retrieve<int[]>().Value[0];
        }
        
        [Fact]
        void TestDependencyInjection()
        {
            Group group = new Group();
            Traceable<Vector2> v = new Traceable<Vector2>(new Vector2(4, 5));
            var drawable = new TestDrawable();
            var dc = new Linkable<DependencyCache>(new DependencyCache());
            
            dc.Value.Store(v);
            group.LinkDependencies(dc);
            Assert.NotNull(group.DependencyCache);
            group.Add (drawable);

            Assert.Equal(new Vector2(4, 5), drawable.Test);
        }


        [Fact]
        void TestUpstreamAssignment()
        {
            Group group = new Group();
            Traceable<Vector2> v = new Traceable<Vector2>(new Vector2(4, 5));
            var drawable = new TestDrawable();
            var dc = new Linkable<DependencyCache>(new DependencyCache());

            dc.Value.Store(v);
            group.LinkDependencies(dc);
            Assert.NotNull(group.DependencyCache);
            group.Add(drawable);

            Assert.Equal(new Vector2(4, 5), drawable.Test);

            v.Value = new Vector2(6, 9);
            Assert.Equal(new Vector2(6, 9), drawable.Test);
        }


        [Fact]
        void TestUpstreamMutation()
        {
            Group group = new Group();
            Traceable<int[]> v = new Traceable<int[]>(new[] { 5 });
            var drawable = new TestDrawable();
            var dc = new Linkable<DependencyCache>(new DependencyCache());
            
            dc.Value.Store(v);
            group.LinkDependencies(dc);
            Assert.NotNull(group.DependencyCache);
            group.Add (drawable);

            Assert.Equal(5, drawable.Test2);

            v.Value[0] = 69;
            Assert.Equal(69, drawable.Test2);
        }

        [Fact]
        void TestNestedDependencyInjection()
        {
             var dc = new Linkable<DependencyCache>(new DependencyCache());
             Traceable<Vector2> v = new Traceable<Vector2>(new Vector2(4, 5));
             dc.Value.Store(v);
             
             Group groupA = new Group();
             Group groupB = new Group();

             TestDrawable drawable = new TestDrawable();

             groupA.LinkDependencies(dc);
             
             groupB.Add(drawable);
             groupA.Add(groupB);
             
             Assert.Equal(new Vector2(4,5), drawable.Test);
             
             v.Value = new Vector2(7, 8);
             Assert.Equal(new Vector2(7,8), drawable.Test);
        }

        class TestGroup : Group
        {
            public int Test 
            { 
                get => DependencyCache.Retrieve<int>().Value;
                set => DependencyCache.Retrieve<int>().Value = value;
            }
        }
        
        [Fact]
        void TestTreeStructure()
        {
            var cache = new Linkable<DependencyCache>(new DependencyCache());
            cache.Value.Store(new Traceable<int>(0));

            var groupA = new TestGroup();
            var groupB = new TestGroup();

            groupA.LinkDependencies(cache); 

            groupA.Add(groupB);
            //Assert.NotSame(groupA.DependencyCache, groupB.DependencyCache);

            Assert.Equal(0, groupA.Test);
            Assert.Equal(0, groupB.Test);

            groupA.Test = 3;
            Assert.Equal(3, groupA.Test);
            Assert.Equal(3, groupB.Test);

            groupB.Test = 5;
            Assert.Equal(5, groupB.Test);
            Assert.Equal(3, groupA.Test);
        }
    }
}