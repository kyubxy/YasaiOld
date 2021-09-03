using System;
using System.Collections.Generic;

namespace Yasai.Resources
{
    public class ContentStore
    {
        private string root;

        private const string MANAGER = "manager.txt";

        private Dictionary<string, object> resources;
        
        public ContentStore(string root)
        {
            this.root = root;
            resources = new Dictionary<string, object>();
        }

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
            throw new NotImplementedException();
        }

        /// <summary>
        ///  reads the manager and finds all paths under a group to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
            
        }
    }
}
