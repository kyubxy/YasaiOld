namespace Yasai.Structures
{
    /// <summary>
    /// Provides a one way bind from one variable to another
    /// </summary>
    public class Linkable<T> : Traceable<T>
    {
        public Linkable(T init) : base(init) 
        { }

        public Linkable()
        { }

        /// <summary>
        /// Links the dependency of a child to that of a parent as in 
        /// Child -> Parent --
        /// where -> means "LinkTo"
        /// </summary>
        /// <param name="parent"></param>
        public void LinkTo(Linkable<T> parent)
        {
            Value = parent.Value;
            parent.Change += ParentOnChange;
        }

        private void ParentOnChange(T value) => Value = value;
    }
}