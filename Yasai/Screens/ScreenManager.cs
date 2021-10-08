using System;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Screens
{
    public class ScreenManager : Drawable, IGroup
    {
        public Screen CurrentScreen { get; private set; }

        private ContentCache _contentCache;
        
        public override bool Loaded => _contentCache != null;

        public bool IgnoreHierarchy { get; }

        public event EventHandler OnScreenChange;

        public ScreenManager(Screen s, bool ignoreHierachy = false)
        {
            CurrentScreen = s;
            IgnoreHierarchy = ignoreHierachy;
        }

        public ScreenManager() : this (new Screen()) { }
        
        public override void Load(ContentCache cache)
        {
            base.Load(cache);
            CurrentScreen.Load(cache);
            _contentCache = cache;
        }

        public override void LoadComplete()
        {
            base.LoadComplete();
            CurrentScreen.LoadComplete();
        }

        public void PushScreen(Screen s)
        {
            if (Loaded)
                s.Load(_contentCache);
            
            s.LoadComplete();

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
        public void MouseDown(MouseArgs args) => CurrentScreen.MouseDown(args);
        public void MouseUp(MouseArgs args) => CurrentScreen.MouseUp(args);
        public void MouseMotion(MouseArgs args) => CurrentScreen.MouseMotion(args);
        public void KeyUp(KeyArgs key) => CurrentScreen.KeyUp(key);
        public void KeyDown(KeyArgs key) => CurrentScreen.KeyDown(key);
    }
}