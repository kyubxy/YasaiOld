using System;

namespace Yasai.Structures 
{
    public interface IBindable <T>
    {
        T Value { get; set; }

        BindStatus BindStatus { get; }
        
        /// <summary>
        /// Bind to another bindable in a bidirectional fashion
        /// </summary>
        /// <param name="other">the object to share values with</param>
        void Bind(IBindable<T> other);
        
        /// <summary>
        /// Bind to another bindable in a unidirectional fashion.
        /// In this relationship, this bindable is readonly and only contains
        /// the values of its master
        /// </summary>
        /// <param name="master">the bindable to read values from</param>
        void BindTo(IBindable<T> master);
        
        /// <summary>
        /// Unbind this bindable from all other bindables
        /// </summary>
        void Unbind();
        
        /// <summary>
        /// Value of bindable has been changed
        /// </summary>
        public event Action<T> OnSet;
        
        /// <summary>
        /// Value of bindable has mutated
        /// </summary>
        public event Action<T> OnChanged;
        
        /// <summary>
        /// Value of bindable has been retrieved
        /// </summary>
        public event Action OnGet;
    }

    public enum BindStatus
    {
        Unidirectional,
        Bidirectional,
        Unbound
    }
}


