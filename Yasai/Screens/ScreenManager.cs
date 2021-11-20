using System;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Screens
{
    public class ScreenManager : Drawable, IContainer
    {
        public Screen CurrentScreen { get; private set; }

        public bool IgnoreHierarchy { get; }

        public event EventHandler OnScreenChange;

        public ScreenManager(Screen s, bool ignoreHierachy = false)
        {
            CurrentScreen = s;
            IgnoreHierarchy = ignoreHierachy;
        }

        public ScreenManager() : this (new Screen()) { }
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            CurrentScreen.Load(dependencies);
        }

        public void PushScreen(Screen s)
        {
            if (Loaded)
                s.Load(Dependencies);

            CurrentScreen = s;
            OnScreenChange?.Invoke(this, new ScreenArgs(s));
        }


        public override void Update()
        {
            if (Enabled)
                CurrentScreen.Update();
        }

        public override void Draw(IntPtr renderer)
        {
            if (Enabled && Visible)
                CurrentScreen.Draw(renderer);
        }

        // screenmanagers can only handle one screen at a time
        // thus, they all ignore the hierarchy
        public override void MouseDown(MouseArgs args)    => CurrentScreen.MouseDown(args);
        public override void MouseUp(MouseArgs args)      => CurrentScreen.MouseUp(args);
        public override void MouseMotion(MouseArgs args)  => CurrentScreen.MouseMotion(args);
        public override void KeyUp(KeyArgs key)           => CurrentScreen.KeyUp(key);
        public override void KeyDown(KeyArgs key)         => CurrentScreen.KeyDown(key);
    }
}