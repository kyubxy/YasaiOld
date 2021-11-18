using System;

namespace Yasai.Structures.DI
{
    class UnresolvableException : Exception
    {
        public UnresolvableException()
        { }
        
        public UnresolvableException(string message)
            : base(message) 
        { }

        public UnresolvableException(string message, Exception inner) 
            : base(message, inner)
        { }

    }
}