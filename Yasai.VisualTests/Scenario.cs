using System;
using Yasai.Screens;

namespace Yasai.VisualTests
{
    public class Scenario : Screen
    {
        public string Name => GetType().Name;
    }

    /// <summary>
    /// All <see cref="Scenario"/>s intended for testing should have this attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TestScenario : Attribute
    {
        public string Description;

        public TestScenario(string desc) => Description = desc;

        public TestScenario()
        {
        }
    }
}