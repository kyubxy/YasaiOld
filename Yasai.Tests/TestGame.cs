using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yasai.Tests.Scenarios;
using Yasai.Graphics.Layout;
using Yasai.Input.Keyboard;
using Yasai.Tests.GUI;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        TestPicker picker;
            
        public TestGame() 
            : base (69)
        {
            ScreenManager sm = new ScreenManager(new DrawablePropertyTest());
            Children.Add(sm);
            
            // find all tests
            Type[] scenarios = GetTests(Assembly.GetExecutingAssembly()).ToArray();
            Children.Add(picker = new TestPicker(this, sm, scenarios));
        }

        static IEnumerable<Type> GetTests(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TestScenario), true).Length > 0)
                    yield return type;
            }
        }

        public override void KeyDown(KeyCode key)
        {
            base.KeyDown(key);
            if (key == KeyCode.TAB)
                picker.Enabled = !picker.Enabled;
        }
    }
}