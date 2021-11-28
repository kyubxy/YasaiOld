using System;

namespace Yasai.Structures
{
    public class Bindable<T> : IBindable<T>
    {
        public virtual event Action<T> OnSet;
        public virtual event Action<T> OnChanged;
        public virtual event Action OnGet;
        
        public BindStatus BindStatus { get; private set; } 
            = BindStatus.Unbound;
        
        // dependency
        private IBindable<T> dependency;
        public IBindable<T> Dependency
        {
            get => dependency;
            private set
            {
                if (value?.Dependency == this)
                   throw new InvalidOperationException("Circular bindable dependency, try using Bind instead of BindTo");
                
                dependency = value;
            }
        }
        
        // (temporary) internal value
        private T self;
        public T Value 
        {
            get
            {
                RaiseGet();

                if (BindStatus == BindStatus.Unbound)
                    return self;

                return Dependency.Value;
            }
            set
            {
                RaiseSet(value);
                RaiseChanged(value);
                
                switch (BindStatus)
                {
                    case BindStatus.Unidirectional:
                        throw new InvalidOperationException(
                        "Tried to set a unidirectional bindable, try unbinding first or using a bidirectional binding");
                    case BindStatus.Bidirectional:
                        if (Dependency == null)
                            self = value;
                        else
                            dependency.Value = value;
                        break;
                    case BindStatus.Unbound:
                        self = value;
                        break;
                }
            } 
        }
        
        public Bindable(T initialSelf) => self = initialSelf;
        
        public Bindable()
        { }
        
        public virtual void Bind(IBindable<T> other)
        {
            // this is like fairy magic to me wtf
            Unbind();
            Dependency = other;
            BindStatus = BindStatus.Bidirectional;
        }

        public virtual void BindTo(IBindable<T> master)
        {
            Unbind();
            
            Dependency = master;
            BindStatus = BindStatus.Unidirectional;
        }

        public virtual void Unbind()
        {
            if (BindStatus == BindStatus.Unbound)
                return;
            
            self = Dependency.Value;
            Dependency = null;
            BindStatus = BindStatus.Unbound;
        }

        // TODO: will need to make these thread safe later
        protected void RaiseGet() => OnGet?.Invoke();
        protected void RaiseSet(T t) => OnSet?.Invoke(t);
        protected void RaiseChanged(T t) => OnChanged?.Invoke(t);
    }
}