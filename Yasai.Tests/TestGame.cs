using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Yasai.Tests.Scenarios;
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

        private string lastScreen;

        private string prefPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YasaiTests", "prefs");
        
        ScreenManager sm;
        
        public TestGame() 
            : base (69)
        {
            Screen last = new WelcomeScreen();
            if (File.Exists(prefPath))
            {
                lastScreen = File.ReadAllText(prefPath);
                if (lastScreen != "")
                    last = (Screen)Assembly.GetExecutingAssembly().CreateInstance(lastScreen);
            }
            else
            {
                Directory.CreateDirectory(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YasaiTests"));
                File.WriteAllLines(prefPath, new string []{});
            }
            
            // find all tests
            sm = new ScreenManager(last);
            Children.Add(sm);
            
            Type[] scenarios = GetTests(Assembly.GetExecutingAssembly()).ToArray();
            Children.Add(picker = new TestPicker(this, sm, scenarios));

            Children.Add(bar = new StatusBar(this));
            sm.OnScreenChange += screenChange;
        }

        private void screenChange(object sender, EventArgs e)
        {
            bar.UpdateTitle(sm.CurrentScreen.GetType().Name);
            ScreenArgs a = (ScreenArgs)e;
            File.WriteAllText(prefPath, a.Screen.GetType().FullName);
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