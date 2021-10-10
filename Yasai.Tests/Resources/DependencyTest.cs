using System.Collections.Generic;
using Xunit;
using Yasai.Resources;
using Yasai.Structures;

namespace Yasai.Tests.Resources
{
    public class DependencyTest
    {
        /// <summary>
        /// Retrieve the number 4 with only its type
        /// </summary>
        [Fact]
        void TestGetDependency()
        {
            DependencyCache dc = new DependencyCache();
            dc.Store(4);
            var retrieve = dc.Retrieve<int>();
            Assert.Equal(4, retrieve);
        }

        /// <summary>
        /// Store and retrieve values with a custom context
        /// </summary>
        [Fact]
        void TestContextedDependencies()
        {
            DependencyCache dc = new DependencyCache();
            dc.Store(4, "number");
            dc.Store(69);
            var retrieveContexted = dc.Retrieve<int>("number");
            var retrieve = dc.Retrieve<int>();
            Assert.Equal(4, retrieveContexted);
            Assert.Equal(69, retrieve);
        }

        /// <summary>
        /// Attempting to retrieve dependencies that have not been stored should
        /// throw a <see cref="KeyNotFoundException"/>
        /// </summary>
        [Fact]
        void TestMissingDependency()
        {
            DependencyCache dc = new DependencyCache();
            Assert.Throws<KeyNotFoundException>(() => dc.Retrieve<int>());
            
            dc.Store(true, "wow");
            Assert.Throws<KeyNotFoundException>(() => dc.Retrieve<bool>("owo"));
        }
    }
}