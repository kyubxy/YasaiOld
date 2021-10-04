using Yasai.Input.Keyboard;
using Yasai.Screens;

namespace Yasai.Debug
{
    public class DebugOverlay : Screen
    {
        public FrameRateCounter FrameRateCounter { get; }
        public CacheVisualiser CacheVisualiser { get; }

        public DebugOverlay()
        {
            Add(FrameRateCounter = new FrameRateCounter()
            {
                Enabled = false
            });
            
            Add (CacheVisualiser = new CacheVisualiser()
            {
                Enabled = false
            });
        }
        public override void KeyDown(KeyArgs key)
        {
            base.KeyDown(key);
            
            // toggle frame rate
            if (key.IsPressed(KeyCode.LCTRL) && key.IsPressed(KeyCode.F11))
                FrameRateCounter.Enabled = !FrameRateCounter.Enabled;
        }
    }
}