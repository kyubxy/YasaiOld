using System;

namespace Yasai.Structures
{
    public interface ITraceable 
    {
        Type ValueType { get; }
        event Action Change;
    }
    
    public class Traceable<T> : ITraceable
    {
        public Type ValueType => typeof(T);
        public event Action Change;

        private T v;
        public virtual T Value
        {
            get => v;
            set
            {
                Change?.Invoke();
                v = value;
            }
        }

        public Traceable(T init) => v = init;
        protected Traceable() {}
    }
}