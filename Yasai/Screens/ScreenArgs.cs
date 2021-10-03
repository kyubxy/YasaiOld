using System;

namespace Yasai.Screens
{
    public class ScreenArgs : EventArgs
    {
        public Screen Screen { get; }
        
        public ScreenArgs(Screen s) => Screen = s;
    }
}