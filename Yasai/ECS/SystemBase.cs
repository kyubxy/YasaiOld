using System.Collections.Generic;

namespace Yasai.ECS
{
    public class SystemBase <T> where T : Component
    {
        protected static List<T> Components = new List<T>();

        public static void Register(T component) => Components.Add(component);
    }
}