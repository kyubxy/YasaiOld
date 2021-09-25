using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yasai.Tests.Scenarios;
using Yasai.Graphics.Layout;
using Yasai.Graphics.Layout.Screens;
using Yasai.Input.Keyboard;
using Yasai.Resources;
using Yasai.Resources.Loaders;
using Yasai.Tests.GUI;

namespace Yasai.Tests
{
    public class TestGame : Game
    {
        private TestPicker picker;
        private StatusBar bar;
            
        public TestGame() 
            : base (69)
        {
            // find all tests
            ScreenManager sm = new ScreenManager(new WelcomeScreen());
            Children.Add(sm);
            
            Type[] scenarios = GetTests(Assembly.GetExecutingAssembly()).ToArray();
            Children.Add(picker = new TestPicker(this, sm, scenarios));

            Children.Add(bar = new StatusBar(this));
            sm.OnScreenChange += (_, _) => { bar.UpdateTitle(sm.CurrentScreen.GetType().Name); };
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            cache.LoadResource("tahoma.ttf","fnt_smallFont", new FontArgs(15));
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