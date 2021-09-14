using System;
using Yasai.Resources;

namespace Yasai.Graphics.Layout
{
    public class ScreenManager : Drawable
    {
        public Screen CurrentScreen { get; private set; }

        private ContentStore _contentStore;

        public ScreenManager(Screen s) => CurrentScreen = s;
        public ScreenManager () => CurrentScreen = new Screen();

        public override bool Loaded => _contentStore != null;

        public override void Load(ContentStore cs)
        {
            // load in the declarative style
            _contentStore = cs;
            CurrentScreen.Load(cs);
        }
        
        public void PushScreen(Screen s)
        {
            CurrentScreen.Dispose();
            
            // load in the imperative style
            if (Loaded)
                s.Load(_contentStore);
            
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
    }
}