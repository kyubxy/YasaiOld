using System;
using Yasai.Graphics.Layout.Groups;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics.Layout.Screens
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

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            
            // load in the declarative style
            CurrentScreen.Start(cache);
        }
        
        public override void Load(ContentCache cache)
        {
            base.Load(cache);
            CurrentScreen.Load(cache);
            _contentCache = cache;
        }
        
        public void PushScreen(Screen s)
        {
            CurrentScreen.Dispose();
            
            // load in the imperative style
            s.Start(_contentCache);
            if (Loaded)
            {
                s.Load(_contentCache);
            }

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
        public override void Dispose() => CurrentScreen.Dispose();
        public void MouseDown(MouseArgs args) => CurrentScreen.MouseDown(args);
        public void MouseUp(MouseArgs args) => CurrentScreen.MouseUp(args);
        public void MouseMotion(MouseArgs args) => CurrentScreen.MouseMotion(args);
        public void KeyUp(KeyArgs key) => CurrentScreen.KeyUp(key);
        public void KeyDown(KeyArgs key) => CurrentScreen.KeyDown(key);
    }
}