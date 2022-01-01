using System;
using System.Collections.Generic;

namespace Yasai.Resources.Stores
{
    /// <summary>
    /// Object representation of the manager.json. Maps a string to a list of paths.
    /// </summary>
    [Serializable]
    public class StorePrefs
    {
        public Dictionary<string, List<string>> Groups;

        public StorePrefs(Dictionary<string, List<string>> g) => Groups = g;

        public StorePrefs() => Groups = new Dictionary<string, List<string>>();

        public bool Empty => Groups.Count == 0;
    }
}