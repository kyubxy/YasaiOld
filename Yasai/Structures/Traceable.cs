using System;

namespace Yasai.Structures
{
    public interface ITraceable 
    {
        Type ValueType { get; }
    }
    
    public class Traceable<T> : ITraceable
    {
        public Type ValueType => typeof(T);
        public event Action<T> Change;

        private T v; 
        public virtual T Value
        {
            get
            {
                if (v == null) 
                    throw new NullReferenceException("Traceable value was null when accessed");
                return v;
            }
            set
            {
                Change?.Invoke(value);
                v = value;
            }
        }

        public Traceable(T init) => v = init;
        public Traceable() { }
    }
}