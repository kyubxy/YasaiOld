using System;

namespace Yasai
{
    public interface ITracable
    {
        Type ValueType { get; }
    }
    
    public class Tracable<T> : ITracable
    {
        public event Action<T> Change;
        public Type ValueType => typeof(T);

        private T v;
        public T Value
        {
            get => v;
            set
            {
                Change?.Invoke(value);
                v = value;
            }
        }

        public Tracable(T init) => Value = init;
    }
}