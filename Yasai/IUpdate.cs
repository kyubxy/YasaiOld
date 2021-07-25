namespace Yasai
{
    public interface IUpdate
    {
        /// <summary>
        /// Determines whether the <see cref="Update()"> function runs
        /// </summary>
        /// <value></value>
        bool Active { get; set; }

        // TODO: gametime
        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update();
    }
}