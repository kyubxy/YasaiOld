using System;
using Yasai.Resources;

namespace Yasai.Graphics.Layout
{
    public class ScreenManager : Drawable
    {
        private Screen currentScreen;

        private ContentStore _contentStore;

        public ScreenManager(Screen s)
        {
            currentScreen = s;
        }

        public void PushScreen(Screen s)
        {
            Loaded = false;
            currentScreen.Dispose();
            s.Load(_contentStore);
            currentScreen = s;
            Loaded = true;
        }
        
        public override void Update()
        {
            if (Enabled)
                currentScreen.Update();
        }

        public override void Draw(IntPtr renderer)
        {
            if (Visible)
                currentScreen.Draw(renderer);
        }

        public override void Dispose()
        {
            currentScreen.Dispose();
        }

        public override void Load(ContentStore cs)
        {
            _contentStore = cs;
            currentScreen.Load(cs);
            Loaded = true;
        }
    }
}