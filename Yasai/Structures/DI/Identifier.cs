using System;

namespace Yasai.Structures.DI
{
    readonly struct Identifier
    {
        public readonly Type Type;
        public readonly string Name;

        public Identifier(Type type, String name)
        {
            Type = type;
            Name = name;
        }

        public override string ToString() => "{" + $"Type: {Type}, Name: {Name}" + "}";
    }
}