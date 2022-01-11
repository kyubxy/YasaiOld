namespace Yasai.Graphics.Text
{
    public interface IText
    {
        /// <summary>
        /// Spacing between words
        /// </summary>
        int WordSpacing { get; set; }

        /// <summary>
        /// Additional spacing between characters
        /// </summary>
        int Spacing { get; set; }

        /// <summary>
        /// Scale of characters
        /// </summary>
        float CharScale { get; set; }
        
        /// <summary>
        /// Align
        /// </summary>
        Align TextAlign { get; set; }
    }
}