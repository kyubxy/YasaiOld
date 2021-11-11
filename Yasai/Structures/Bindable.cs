using System;
using System.ComponentModel;

namespace Yasai.Structures
{
    public class Bindable<T> : IBindable<T>
    {
        public event Action<T> OnSet;
        public event Action<T> OnChanged;
        public event Action OnGet;
        
        public BindStatus BindStatus { get; private set; } 
            = BindStatus.Unbound;
        
        private IBindable<T> other;
        
        private T value;
        public T Value 
        {
            get
            {
                OnGet?.Invoke();
                return value;
            }
            set
            {
                OnSet?.Invoke(value);
                OnChanged?.Invoke(value);
                this.value = value;
            } 
        }
        
        public Bindable(T initialValue) => value = initialValue;
        public Bindable()
        { }
        
        public void Bind(IBindable<T> other)
        {
            this.other = other;
            BindStatus = BindStatus.Bidirectional;
        }

        public void BindTo(IBindable<T> master)
        {
            other = master;
            BindStatus = BindStatus.Unidirectional;
        }

        public void Unbind()
        {
            BindStatus = BindStatus.Unbound;
        }
    }
}