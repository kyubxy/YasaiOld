namespace Yasai.Graphics.Layout
{
    public class ScreenManager : ISpriteBase
    {
        public bool Enabled { get; set; }
        private Screen currentScreen;

        public ScreenManager(Screen s)
        {
            currentScreen = s;
        }

        public void PushScreen(Screen s)
        {
            Loaded = false;
            currentScreen.Dispose();
            s.Load();
            currentScreen = s;
            Loaded = true;
        }
        
        public void Update()
        {
            if (Enabled)
                currentScreen.Update();
        }

        public bool Visible { get; set; }
        public void Draw()
        {
            if (Visible)
                currentScreen.Draw();
        }

        public void Dispose()
        {
            currentScreen.Dispose();
        }

        public bool Loaded { get; private set; }
        public void Load()
        {
            currentScreen.Load();
            Loaded = true;
        }
    }
}