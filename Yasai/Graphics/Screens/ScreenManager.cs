using System;
using OpenTK.Windowing.Common;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Screens
{
    public class ScreenManager : Drawable
    {
        public Screen CurrentScreen { get; private set; }

        public event EventHandler OnScreenChange;

        public ScreenManager(Screen s)
        {
            CurrentScreen = s;
        }

        public ScreenManager() : this (new Screen()) { }

        private DependencyContainer dependencies;
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            CurrentScreen.Load(dependencies);
            this.dependencies = dependencies;
        }

        public void PushScreen(Screen s)
        {
            if (Loaded)
                s.Load(dependencies);

            CurrentScreen = s;
            OnScreenChange?.Invoke(this, new ScreenArgs(s));
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            
            if (Enabled)
                CurrentScreen.Update(args);
        }

        public sealed override void Draw()
        {
            base.Draw();
            
            if (Enabled && Visible)
                CurrentScreen.Draw();
        }

        // screenmanagers can only handle one screen at a time
        // thus, they all ignore the hierarchy
        //public override void MouseDown(MouseArgs args)    => CurrentScreen.MouseDown(args);
        //public override void MouseUp(MouseArgs args)      => CurrentScreen.MouseUp(args);
        //public override void MouseMotion(MouseArgs args)  => CurrentScreen.MouseMotion(args);
        //public override void KeyUp(KeyArgs key)           => CurrentScreen.KeyUp(key);
        //public override void KeyDown(KeyArgs key)         => CurrentScreen.KeyDown(key);
    }
}