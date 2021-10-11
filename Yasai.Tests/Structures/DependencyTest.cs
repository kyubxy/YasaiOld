using System.Collections.Generic;
using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Structures
{
    public class DependencyTest
    {
        /// <summary>
        /// Retrieve the number 4 with only its type
        /// </summary>
//        [Fact]
        void TestGetDependency()
        {
            DependencyCache dc = new DependencyCache();
            dc.Store(new Traceable<int>(4));
            var retrieve = dc.Retrieve<int>();
            Assert.Equal(4, retrieve.Value);
        }

 //       [Fact]
        void TestGetChangingDependency()
        {
            DependencyCache dc = new DependencyCache();
            Traceable<int> dependency = new Traceable<int>(5);
            dc.Store(dependency);
            Assert.Equal(5, dc.Retrieve<int>().Value);
            dependency.Value = 7;
            Assert.Equal(7, dc.Retrieve<int>().Value);
        }

//        [Fact]
        void TestGetDependencyWithMutations()
        {
            DependencyCache dc = new DependencyCache();
            Traceable<int[]> dependency = new Traceable<int[]>(new[] { 5 });
            dc.Store(dependency);
            var retrieve = dc.Retrieve<int[]>();
            Assert.Equal(new [] { 5 }, retrieve.Value);
            dependency.Value[0] = 7;
            Assert.Equal(new [] { 7 }, retrieve.Value);
        }

        /// <summary>
        /// Store and retrieve values with a custom context
        /// </summary>
//        [Fact]
        void TestContextedDependencies()
        {
            DependencyCache dc = new DependencyCache();
            dc.Store(new Traceable<int>(4), "number");
            dc.Store(new Traceable<int>(69));
            var retrieveContexted = dc.Retrieve<int>("number");
            var retrieve = dc.Retrieve<int>();
            Assert.Equal(4, retrieveContexted.Value);
            Assert.Equal(69, retrieve.Value);
        }

        /// <summary>
        /// Attempting to retrieve dependencies that have not been stored should
        /// throw a <see cref="KeyNotFoundException"/>
        /// </summary>
//        [Fact]
        void TestMissingDependency()
        {
            DependencyCache dc = new DependencyCache();
            Assert.Throws<KeyNotFoundException>(() => dc.Retrieve<int>());
            
            dc.Store(new Traceable<bool>(true), "wow");
            Assert.Throws<KeyNotFoundException>(() => dc.Retrieve<bool>("owo"));
        }
    }
}