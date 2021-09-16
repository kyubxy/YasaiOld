using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Yasai.Resources.Loaders;

namespace Yasai.Resources
{
    public class ContentCache : IDisposable, ILoad
    {
        public string Root { get; }

        private string MANAGER => "manager.json";
        private ContentManager manager;

        private string resourcePath => Path.Combine(Directory.GetCurrentDirectory(), Root);
        
        public bool Loaded => manager != null;
        
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
        /// Loads a single directory from the path and adds it to the internal dictionary
        /// </summary>
        /// <param name="path">path to resource</param>
        /// <param name="_key">dictionary key, how to reference the resource, blank entries default to the extensionless resource filename</param>
        /// <param name="args">additional load arguments supported by the loader</param>
        /// <param name="hushWarnings">whether the program should report unloadable resources</param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadResource(string path, string _key = null, ILoadArgs args = null, bool hushWarnings = false)
        {
            // TODO: prevent double loading, maybe store the path somewhere
            string key = _key == null ? Path.GetFileNameWithoutExtension(path) : _key;
            ILoader loader = Loaders.Find(x => x.FileTypes.Contains(Path.GetExtension(path).ToLower()));

            // can't find any loaders
            if (loader == null)
            {
                if (!hushWarnings)
                    Console.WriteLine($"cannot load file of type {Path.GetExtension(path)}, it was subsequently skipped");
                return;
            }

            resources[key] = loader.GetResource(game, Path.Combine(resourcePath, path), args);
        }

        /// <summary>
        ///  reads the manager and finds all paths under a group to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
            // TODO: load resources from files
            if (manager.Empty)
            {
                Console.WriteLine("Either the manager is empty or it was not loaded when LoadResources was called");
                return;
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads *all* of the resources in the root.
        /// Use this only for smaller projects, otherwise, avoid this at all costs and use functions like
        /// <see cref="LoadResources"/> to load in larger amounts of assets at once or <see cref="LoadResource"/>
        /// <param name="readManager">whether it should read the manager.txt for all resources</param>
        /// </summary>
        public void LoadAll(bool readManager = true)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (IResource x in resources.Values) 
                x.Dispose();
        }

        public void Load(ContentCache cache)
        {
            string path = Path.Combine(resourcePath, MANAGER);

            if (File.Exists(path))
                manager = JsonSerializer.Deserialize<ContentManager>(path);
            else
                manager = new ContentManager();
        }

        /// <summary>
        /// Write the manager to the resource path.
        /// Will overwrite the previous written manager.
        /// Will not overwrite the current manager.
        /// Will not write an empty manager
        /// </summary>
        public void Write()
        {
            if (manager.Empty)
                Console.WriteLine("The manager is empty, aborting the write process..");
            else
            {
                string jsonStr = JsonSerializer.Serialize(manager);
                File.WriteAllText(Path.Combine (resourcePath, "manager_written.txt"), jsonStr);
            }
        }
    }
}
