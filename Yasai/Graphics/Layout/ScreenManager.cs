using System;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics.Layout
{
    public class ScreenManager : Drawable, IKeyListener, IMouseListener
    {
        public Screen CurrentScreen { get; private set; }

        private ContentCache _contentCache;

        public ScreenManager(Screen s) => CurrentScreen = s;
        public ScreenManager () => CurrentScreen = new Screen();

        public override bool Loaded => _contentCache != null;

        public override void Load(ContentCache cache)
        {
            // load in the declarative style
            _contentCache = cache;
            CurrentScreen.Load(cache);
        }
        
        public void PushScreen(Screen s)
        {
            CurrentScreen.Dispose();
            
            // load in the imperative style
            if (Loaded)
                s.Load(_contentCache);
            
            CurrentScreen = s;
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

        public override void Dispose()
        {
            CurrentScreen.Dispose();
        }


        public bool IgnoreHierachy { get; }
        
        public void MouseDown(MouseArgs args)
        {
            CurrentScreen.MouseDown(args);
        }

        public void MouseUp(MouseArgs args)
        {
            CurrentScreen.MouseUp(args);
        }

        public void MouseMotion(MouseArgs args)
        {
            CurrentScreen.MouseMotion(args);
        }

        public void KeyUp(KeyCode key)
        {
            CurrentScreen.KeyUp(key);
        }

        public void KeyDown(KeyCode key)
        {
            CurrentScreen.KeyUp(key);
        }
    }
}