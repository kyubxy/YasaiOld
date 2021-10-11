using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Yasai.Resources.Loaders;

namespace Yasai.Resources
{
    public class ContentCache : ILoad
    {
        public string Root { get; }

        private string MANAGER => "manager.json";
        private ContentManager manager;

        private string resourcePath => Path.Combine(Directory.GetCurrentDirectory(), Root);
        
        public bool Loaded => manager != null;
        
        private Dictionary<string, Resource> resources;

        protected List<ILoader> Loaders;

        public Game Game;
        
        public ContentCache(Game game, string root = "Assets")
        {
            Root = root;
            resources = new Dictionary<string, Resource>();
            Game = game;
            
            // add the loaders
            Loaders = new List<ILoader>();
            Loaders.Add(new ImageLoader());
            Loaders.Add(new FontLoader());
        }

        /// <summary>
        /// get a *preloaded* resource from the internal dictionary
        /// </summary>
        /// <param name="res">the resource key to look for</param>
        /// <typeparam name="T">the expected resource type</typeparam>
        /// <returns>the resource</returns>
        /// <exception cref="DirectoryNotFoundException">thrown if the key was not present in the dictionary</exception>
        public T GetResource<T>(string res) where T : Resource
        {
            if (!resources.ContainsKey(res))
            {
                throw new DirectoryNotFoundException($"{res} was not loaded into the dictionary. " + 
                                                     $"Ensure you preload it with LoadResource or some similar function");
            }

            return (T)resources[res];
        }

        /// <summary>
        /// Loads a single resource from the path and adds it to the internal dictionary
        /// </summary>
        /// <param name="path">path to resource</param>
        /// <param name="_key">dictionary key, how to reference the resource, blank entries default to the extensionless resource filename</param>
        /// <param name="args">additional load arguments supported by the loader</param>
        /// <param name="hushWarnings">whether the program should report unloadable resources</param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadResource(string path, string _key = null, ILoadArgs args = null, bool hushWarnings = false)
        {
            string key = _key ?? Path.GetFileNameWithoutExtension(path);
            string loadType = Path.GetExtension(path);
            ILoader loader = Loaders.Find(x => x.FileTypes.Contains(loadType.ToLower()));

            // can't find any loaders
            if (loader == null)
            {
                if (!hushWarnings)
                    Console.WriteLine($"cannot load file of type {loadType}, it was subsequently skipped");
                return;
            }
            
            string loadAbsPath = Path.Combine(resourcePath, path);
            if (IsResourceLoaded(loadAbsPath, args ?? loader.DefaultArgs))
            {
                if (!hushWarnings)
                    Console.WriteLine($"file {path} is already loaded with similar arguments, " +
                                      $"no new resource was loaded");
                return;
            }
            
            resources[key] = loader.GetResource(Game, loadAbsPath, args);
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
            if(readManager) throw new NotImplementedException();

            IEnumerable<string> filenames = Directory.EnumerateFiles(Root);
            foreach(string n in filenames)
                LoadResource(n);
        }

        /// <summary>
        /// Dispose of a specific resource
        /// </summary>
        /// <param name="key"></param>
        public void Unload(string key)
        {
            if (!resources.ContainsKey(key))
                Console.WriteLine($"no such {key} in dictionary");
            else
                resources[key].Dispose();
        }

        /// <summary>
        /// Unload all resources
        /// </summary>
        public void Dispose()
        {
            foreach (Resource x in resources.Values) 
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
        
        public void LoadComplete() { }

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
        
        private bool IsResourceLoaded(string absPath, ILoadArgs args)
        {
            foreach (Resource r in resources.Values)
                if (r.Path == absPath && r.Args == args) 
                    return true;
            
            return false;
        }
    }
}
