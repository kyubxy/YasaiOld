using System;
using Xunit;
using Yasai.Structures;

namespace Yasai.Tests.Resources
{
    public class TraceableTest
    {
        /// <summary>
        /// changing value of num should set numChanged from false to true
        /// </summary>
        [Fact]
        void TestChange()
        {
            Traceable<int> num = new Traceable<int>(5);
            bool numChanged = false;
            num.Change += _ => numChanged = true;
            num.Value = 8;
            Assert.True(numChanged);
        }

        /// <summary>
        /// initialising a traceable without a value should return its default value
        /// </summary>
        [Fact]
        void TestDefault()
        {
            Traceable<int> t = new Traceable<int>();
            Assert.Equal(0, t.Value);
        }
    }
}