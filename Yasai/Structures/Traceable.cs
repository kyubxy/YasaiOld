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
            get => v;
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