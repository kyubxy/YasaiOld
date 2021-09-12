using System;
using Yasai.Resources;

namespace Yasai.Graphics.Layout
{
    public class ScreenManager : Drawable
    {
        private Screen currentScreen;

        private ContentStore _contentStore;

        public ScreenManager(Screen s) => currentScreen = s;
        public ScreenManager () => currentScreen = new Screen();

        public override bool Loaded => _contentStore != null;

        public override void Load(ContentStore cs)
        {
            // load in the declarative style
            _contentStore = cs;
            currentScreen.Load(cs);
        }
        
        public void PushScreen(Screen s)
        {
            currentScreen.Dispose();
            
            // load in the imperative style
            if (Loaded)
                s.Load(_contentStore);
            
            currentScreen = s;
        }
        
        public override void Update()
        {
            if (Enabled)
                currentScreen.Update();
        }

        public override void Draw(IntPtr renderer)
        {
            if (Enabled && Visible)
                currentScreen.Draw(renderer);
        }

        public override void Dispose()
        {
            currentScreen.Dispose();
        }
    }
}