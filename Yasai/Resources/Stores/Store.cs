// Some of the code in this file was also written by caveguy427 (https://github.com/caveguy427)
// Renaming the class has removed previous authorship information on the github repo

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Yasai.Resources.Stores
{
    public abstract class Store<T> : IStore 
        where T : IResource
    {
        // filepath root
        public string Root { get; }

        public StorePrefs Prefs => throw new NotImplementedException();
        
        /// <summary>
        /// valid file types for the store type
        /// </summary>
        public abstract string[] FileTypes { get; }
        
        /// <summary>
        /// default resource arguments for the resource to be loaded
        /// </summary>
        public abstract IResourceArgs DefaultArgs { get; }

        /// <summary>
        /// internal resource dictionary
        /// </summary>
        private readonly Dictionary<string, T> resources;
        
        public Store(string root = "Assets")
        {
            Root = root;
            resources = new Dictionary<string, T>();
        }

        /// <summary>
        /// get a *preloaded* resource from the internal dictionary.
        /// </summary>
        /// <param name="res">the resource key to look for</param>
        /// <typeparam name="T">the expected resource type</typeparam>
        /// <returns>the resource</returns>
        /// <exception cref="DirectoryNotFoundException">thrown if the key was not present in the dictionary</exception>
        public virtual T GetResource(string res)
        {
            if (!resources.ContainsKey(res))
                throw new DirectoryNotFoundException($"{res} was not loaded into the dictionary. " + 
                                                     $"Ensure you preload it with LoadResource or some similar function");
            return resources[res];
        }
        
        /// <summary>
        /// Loads a single resource from the path and adds it to the internal dictionary
        /// the key is set to the extensionless name of the resource
        /// </summary>
        /// <param name="path">path to resource</param>
        public void LoadResource (string path) => LoadResource(path, Path.ChangeExtension(path, null));

        /// <summary>
        /// Loads a single resource from the path and adds it to the internal dictionary
        /// </summary>
        /// <param name="path">path to resource</param>
        /// <param name="key">dictionary key, how to reference the resource</param>
        /// <param name="args">additional load arguments supported by the loader</param>
        /// <exception cref="NotSupportedException"></exception>
        public void LoadResource(string path, string key, IResourceArgs args = null)
        {
            // check if valid resource type
            string loadType = Path.GetExtension(path);
            if (!FileTypes.Contains(loadType))
            {
                GameBase.YasaiLogger.LogWarning(
                    $"Attempted to load file of type {loadType} into a store of type {typeof(T)}, file was subsequently skipped");
                return;
            }

            // avoid loading duplicates
            string loadPath = Path.Combine(Root, path); 
            if (IsResourceLoaded(loadPath, args ?? DefaultArgs))
                return;
            
            // check if resource exists
            if (!File.Exists(loadPath))
                throw new FileNotFoundException($"no such {loadPath} could be found");
            
            var res = AcquireResource(loadPath, args ?? DefaultArgs);
            resources[key] = res;
        }

        /// <summary>
        /// Add a resource directly to the internal dictionary
        /// </summary>
        /// <param name="resource">the resource to add</param>
        /// <param name="key">dictionary key</param>
        public void AddResource(T resource, string key) => resources[key] = resource;

        /// <summary>
        /// how to acquire the resource given a path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected abstract T AcquireResource(string path, IResourceArgs args);

        /// <summary>
        /// reads the manager and finds all paths under a container to load from
        /// </summary>
        /// <param name="group"></param>
        public void LoadResources(string group)
        {
            // TODO: load resources from files
            if (Prefs == null)
            {
                GameBase.YasaiLogger.LogWarning("Either the manager is empty or it was not loaded when LoadResources was called");
                return;
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads *all* of the resources in the root.
        /// Use this only for smaller projects, otherwise, avoid this at all costs and use functions like
        /// <see cref="LoadResources"/> to load in larger amounts of assets at once or <see cref="LoadResource"/>
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
                GameBase.YasaiLogger.LogWarning($"no such {key} in store");
            else
            {
                resources[key].Dispose();
                resources[key] = default;
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

        /// <summary>
        /// Write the manager to the resource path.
        /// Will overwrite the previous written manager.
        /// Will not overwrite the current manager.
        /// Will not write an empty manager
        /// </summary>
        public void Write()
            => throw new NotImplementedException();
        
        /// <summary>
        /// search the resource values for an absolute path and args
        /// </summary>
        /// <param name="absPath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool IsResourceLoaded(string absPath, IResourceArgs args)
        {
            foreach (T r in resources.Values)
                if (r.Path == absPath && r.Args == args) 
                    return true;
            
            return false;
        }
    }
}
