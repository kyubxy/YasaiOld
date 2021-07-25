namespace Yasai.Graphics
{
    public interface IDrawable
    {
        /// <summary>
        /// Whether the <see cref="Draw ()"> function is called
        /// or if the drawable object is visible
        /// </summary>
        /// <value></value>
        public bool Visible { get; set; }

        // TODO: put in a spritebatch or whatever else is required for drawing :v
        /// <summary>
        /// Called once per frame
        /// </summary>
        public void Draw ();
    }
}