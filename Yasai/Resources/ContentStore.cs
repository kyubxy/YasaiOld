using System;
using System.Collections.Generic;

namespace Yasai.Resources
{
    public class ContentStore
    {
        public string Root { get; set; }

        private string MANAGER = "manager.txt";

        private Dictionary<string, object> resources;
        
        public ContentStore(string root)
        {
            this.Root = root;
            resources = new Dictionary<string, object>();
        }
        
        public ContentStore() : this ("Assets") 
        { }

        public T GetResource<T>(string res)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a single resource from the path
        /// </summary>
        /// <param name="path"></param>
        public void LoadResource(string path)
        {
        }

        /// <summary>
        ///  reads the manager and finds all paths under a group to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
        }

        /// <summary>
        /// Loads *all* of the resources in the root.
        /// Use this only for smaller projects, otherwise, avoid this at all costs and use functions like
        /// <see cref="LoadResources"/> to load in larger amounts of assets at once or <see cref="LoadResource"/>
        /// </summary>
        public void LoadAll()
        {
        }
    }
}
