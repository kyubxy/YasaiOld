using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Yasai.Input.Keyboard;
using Yasai.Resources;
using Yasai.Resources.Stores;
using Yasai.Screens;
using Yasai.Structures.DI;
using Yasai.VisualTests.GUI;

namespace Yasai.VisualTests
{
    public class TestGame : Game
    {
        private TestPicker picker;
        private StatusBar bar;

        private string lastScreen;

        private string prefPath => Path.Combine(PrefHelper.HomeDirectory, "prefs");
        
        ScreenManager sm;

        private TextureStore textureStore;
        
        public TestGame()
        {
            textureStore = new TextureStore(Dependencies);
            Dependencies.Register<TextureStore>(textureStore);
            
            // load last screen
            Screen last = new WelcomeScreen();
            if (File.Exists(prefPath))
            {
                lastScreen = File.ReadAllText(prefPath);
                if (lastScreen != "")
                    last = (Screen)Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(lastScreen) 
                                                            ?? typeof(WelcomeScreen));
            }
            else
            {
                Directory.CreateDirectory(
                    Path.Combine(PrefHelper.HomeDirectory, "YasaiTests"));
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

        static IEnumerable<Type> GetTests(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TestScenario), true).Length > 0)
                    yield return type;
            }
        }

        public override void KeyDown(KeyArgs key)
        {
            base.KeyDown(key);
            if (key.IsPressed(KeyCode.TAB) && key.IsPressed(KeyCode.LSHIFT))
            {
                picker.Enabled = !picker.Enabled;
            }
        }
    }
}