using System;
using System.Collections.Generic;

namespace Yasai.Input.Keyboard
{
    public class KeyArgs : EventArgs
    {
        private HashSet<KeyCode> pressed; 
        
        public KeyArgs(HashSet<KeyCode> pressed)
        {
            this.pressed = pressed;
        }

        public bool IsPressed(KeyCode k) => pressed.Contains(k);

        public override string ToString()
        {
            string ret = "[";
            foreach (KeyCode k in pressed)
                ret += $"{k}, ";

            ret += "]";
            return ret;
        }
    }
}