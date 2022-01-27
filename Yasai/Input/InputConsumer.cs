using System;

namespace Yasai.Input
{
    public class InputConsumer : Attribute
    {
        public IInputHandler[] Handlers { get; }
        
        // TODO: cant just use interfaces for culling
        public InputConsumer(IInputHandler[] handlers)
        {
            Handlers = handlers;
        }
    }
}