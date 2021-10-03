using System;

using Yasai.Graphics.Layout;
using Yasai.Graphics.Layout.Screens;

namespace Yasai.Tests.Scenarios
{
    public class Scenario : Screen
    {
        public string Name => GetType().Name;
        protected Game Game;

        public Scenario(Game game)
        {
            Game = game;
        }
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