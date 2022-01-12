using System;
using Xunit;
using Yasai.Structures;
using Yasai.Structures.Bindables;

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

        /// <summary>
        /// assert that a child is bound to its parent
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        public void AssertBound<T>(IBindable<T> child, IBindable<T> parent) => Assert.Equal(child.Dependency, parent);

        /// <summary>
        /// assert that one bindable isn't bound to another
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        public void AssertUnbound<T>(IBindable<T> child, IBindable<T> parent) =>
            Assert.NotEqual(child.Dependency, parent);
        
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

            try
            {
                Assert.Throws<InvalidOperationException>(() => a.Value = 69);
            }
            catch (InvalidOperationException _)
            {
                
            }
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

        //[Fact]
        void testBindEvents()
        {
            TestBindable a = new TestBindable(3);

            int changed = 0;
            int set = 0;
            bool getToggle = false;

            a.OnChanged += i => changed = i;
            a.OnGet += () => getToggle = !getToggle;
            a.OnSet += i => set = i;

            Assert.Equal(0, changed);
            Assert.Equal(0, set);
            Assert.False(getToggle);

            var n = a.Value;
            Assert.True(getToggle);

            a.Value = 69;
            Assert.Equal(69, set);
            Assert.Equal(69, changed);
        }

        [Fact]
        void testChainedUniBinds()
        {
            TestBindable a = new TestBindable();
            TestBindable b = new TestBindable();
            TestBindable c = new TestBindable();
            
            c.BindTo(b);
            b.BindTo(a);

            a.Value = 69;
            
            Assert.Equal(69, a.Value);
            Assert.Equal(69, b.Value);
            Assert.Equal(69, c.Value);
        }

        [Fact]
        void testImplicitUnbind()
        {
             TestBindable a = new TestBindable(23);
             TestBindable b = new TestBindable();
             TestBindable c = new TestBindable();           
             
             c.BindTo(b);

             b.Value = 9;
             
             Assert.Equal(9, b.Value);
             Assert.Equal(9, c.Value);
             
             c.BindTo(a);

             AssertUnbound(c, b);
             AssertBound(c, a);
        }

        [Fact]
        void testCircularDependency()
        {
            TestBindable a = new TestBindable();
            TestBindable b = new TestBindable();           
            
            a.BindTo(b);

            Assert.Throws<InvalidOperationException>(() => b.BindTo(a));
        }
    }
}