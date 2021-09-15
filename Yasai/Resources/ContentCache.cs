using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yasai.Resources.Loaders;

namespace Yasai.Resources
{
    public class ContentCache : IDisposable
    {
        public string Root { get; }

        private string MANAGER = "manager.txt";

        private Dictionary<string, IResource> resources;

        protected List<ILoader> Loaders;

        private Game game;
        
        public ContentCache(string root)
        {
            Root = root;
            resources = new Dictionary<string, IResource>();
            
            // add the loaders
            Loaders = new List<ILoader>();
            Loaders.Add(new ImageLoader());
            Loaders.Add(new FontLoader());
        }

        public ContentCache(Game game) : this("Assets") 
            => this.game = game;

        /// <summary>
        /// get a *preloaded* resource from the internal dictionary
        /// </summary>
        /// <param name="res">the resource key to look for</param>
        /// <typeparam name="T">the expected resource type</typeparam>
        /// <returns>the resource</returns>
        /// <exception cref="DirectoryNotFoundException">thrown if the key was not present in the dictionary</exception>
        public T GetResource<T>(string res)
        {
            if (!resources.ContainsKey(res))
            {
                throw new DirectoryNotFoundException($"{res} was not loaded into the dictionary. " + 
                                                     $"Ensure you preload it with LoadResource or some similar function");
            }

            return (T)resources[res];
        }

        /// <summary>
        /// Loads a single resource from the path
        /// </summary>
        /// <param name="path"></param>
        public void LoadResource(string path, string _key = null, ILoadArgs args = null)
        {
            // TODO: prevent double loading, maybe store the path somewhere
            string key = _key == null ? Path.GetFileNameWithoutExtension(path) : _key;
            ILoader loader = Loaders.Find(x => x.FileTypes.Contains(Path.GetExtension(path).ToLower()));

            // can't find any loaders
            if (loader == null)
                throw new NotSupportedException($"cannot load file of type {Path.GetExtension(path)}"); 
            
            resources[key] = loader.GetResource(game, Path.Combine(Directory.GetCurrentDirectory(), Root, path), args);
        }

        /// <summary>
        ///  reads the manager and finds all paths under a group to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
            // TODO: load resources from files
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads *all* of the resources in the root.
        /// Use this only for smaller projects, otherwise, avoid this at all costs and use functions like
        /// <see cref="LoadResources"/> to load in larger amounts of assets at once or <see cref="LoadResource"/>
        /// </summary>
        public void LoadAll()
        {
            // TODO: load all the resources in a folder
        }

        public void Dispose()
        {
            foreach (IResource x in resources.Values) 
                x.Dispose();
        }
    }
}
