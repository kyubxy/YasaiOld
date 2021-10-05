using System.Collections.Generic;
using System.ComponentModel;

namespace Yasai.ECS
{
    public class Entity
    {
        public HashSet<string> Tags;

        private HashSet<Component> _components;

        public Entity(Component[] c) => _components = new HashSet<Component>(c);
        
        public bool Enabled { get; set; }

        public Entity()
        {
        }

        public void AddComponent(Component c)
        {
            c.Parent = this;
            _components.Add(c);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component c in _components)
                if (c is T) return (T)c;

            return null;
        }
    }
}