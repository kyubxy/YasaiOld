using System;
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
        void TestLayeredDI()
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
        void TestDifferentHandlers()
        {
             var groupA = new TestGroup();
             var groupB = new TestGroup();

             groupA.Add(groupB);
             groupA.DependencyHandler = new DependencyHandler();
             
             Assert.NotSame(groupA.DependencyHandler, groupB.DependencyHandler);
        }
        
        class TestGroup : Group
        {
            public int Test { 
                get => DependencyHandler.Retrieve<int>().Value;
                set => DependencyHandler.Retrieve<int>().Value = value;
            }

            public string bruh;
        }

        [Fact]
        void TestTreeStructure()
        {
             DependencyHandler dh = new DependencyHandler();
             dh.Store(new Tracable<int>(0));
             
             var groupA = new TestGroup();
             groupA.bruh = "A";
             var groupB = new TestGroup();
             groupB.bruh = "B";

             groupA.DependencyHandler = dh;
             
             groupA.Add(groupB);
             
             Assert.Equal(0, groupA.Test);
             Assert.Equal(0, groupB.Test);

             groupA.Test = 3;
             Assert.Equal(3, groupA.Test);
             Assert.Equal(3, groupB.Test);
             
             Assert.NotSame(groupA.DependencyHandler, groupB.DependencyHandler);

             groupB.Test = 5;
             Assert.Equal(5, groupB.Test);
             Assert.Equal(3, groupA.Test);
        }       
    }
}