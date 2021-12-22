namespace Yasai.Graphics.Text
{
    /// <summary>
    /// Store a set of characters in different ways
    /// </summary>
    public class CharacterSet
    {
        private char[] chars;

        private int start;
        private int end;

        private enum InputMethod
        {
            Array,
            StartEnd
        }

        private InputMethod method;
        
        public CharacterSet(int start, int end)
        {
            this.start = start;
            this.end = end;

            method = InputMethod.StartEnd;
        }
        
        public CharacterSet(char[] chars)
        {
            this.chars = chars;
            
            method = InputMethod.Array;
        }
        
        public char[] GetChars()
        {
            return null;
        }
    }
}