using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.Resources
{
    public interface IContentStore 
    {
        string Root { get; }
        ContentPrefs Prefs { get; }
        string[] FileTypes { get; }
        IResourceArgs DefaultArgs { get; }
    }
    
    public abstract class ContentStore<T> : IContentStore 
        where T : Resource
    {
        // filepath root
        public string Root { get; }

        public ContentPrefs Prefs => throw new NotImplementedException();
        
        public abstract string[] FileTypes { get; }
        public abstract IResourceArgs DefaultArgs { get; }

        private Dictionary<string, T> resources;

        private DependencyContainer dependencies;
        
        public ContentStore(DependencyContainer container, string root = "Assets")
        {
            Root = root;
            dependencies = container;
            resources = new Dictionary<string, T>();
        }

        /// <summary>
        /// get a *preloaded* resource from the internal dictionary
        /// </summary>
        /// <param name="res">the resource key to look for</param>
        /// <typeparam name="T">the expected resource type</typeparam>
        /// <returns>the resource</returns>
        /// <exception cref="DirectoryNotFoundException">thrown if the key was not present in the dictionary</exception>
        public T GetResource(string res) 
        {
            if (!resources.ContainsKey(res))
            {
                throw new DirectoryNotFoundException($"{res} was not loaded into the dictionary. " + 
                                                     $"Ensure you preload it with LoadResource or some similar function");
            }

            return resources[res];
        }
        
        public void LoadResource (string path) => LoadResource(path, Path.ChangeExtension(path, null));

        /// <summary>
        /// Loads a single resource from the path and adds it to the internal dictionary
        /// </summary>
        /// <param name="path">path to resource</param>
        /// <param name="key">dictionary key, how to reference the resource</param>
        /// <param name="args">additional load arguments supported by the loader</param>
        /// <param name="hushWarnings">whether the program should report unloadable resources</param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadResource(string path, string key, IResourceArgs args = null)
        {

            
            // check if valid resource type
            string loadType = Path.GetExtension(path);
            if (!FileTypes.Contains(loadType))
            {
                Console.WriteLine(
                    $"Attempted to load file of type {loadType} into a store of type {typeof(T)}, file was subsequently skipped");
                return;
            }

            // avoid loading duplicates
            string loadPath = Path.Combine(Root, path); 
            if (IsResourceLoaded(loadPath, args ?? DefaultArgs))
                return;
            
            // check if resource exists
            if (!File.Exists(loadPath))
                throw new FileNotFoundException();

            resources[key] = AcquireResource(loadPath, args);
        }

        /// <summary>
        /// how to acquire the resource given a path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected abstract T AcquireResource(string path, IResourceArgs args);

        /// <summary>
        ///  reads the manager and finds all paths under a group to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
            // TODO: load resources from files
            if (Prefs == null)
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
        public void LoadAll()
        {
            var filenames = Directory.EnumerateFiles(Root, "*", SearchOption.AllDirectories);
            foreach(string nr in filenames)
            {
                string n = Path.GetRelativePath(Root, nr);
                LoadResource(n);
            }
        }

        /// <summary>
        /// Dispose of a specific resource
        /// </summary>
        /// <param name="key"></param>
        public void Unload(string key)
        {
            if (!resources.ContainsKey(key))
                Console.WriteLine($"no such {key} in store");
            else
            {
                resources[key].Dispose();
                resources[key] = null;
            }
        }

        /// <summary>
        /// Unload all resources
        /// </summary>
        public void Dispose()
        {
            foreach (T x in resources.Values) 
                x.Dispose();
        }

        public void LoadPrefs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write the manager to the resource path.
        /// Will overwrite the previous written manager.
        /// Will not overwrite the current manager.
        /// Will not write an empty manager
        /// </summary>
        public void Write()
        {
            throw new NotImplementedException();
        }
        
        private bool IsResourceLoaded(string absPath, IResourceArgs args)
        {
            foreach (Resource r in resources.Values)
                if (r.Path == absPath && r.Args == args) 
                    return true;
            
            return false;
        }
    }
}