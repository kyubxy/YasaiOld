using System;
using System.Collections;
using System.Collections.Generic;

namespace Yasai.Graphics.Layout
{
    public class Group : ISpriteBase, ICollection<ISpriteBase>
    {
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Loaded { get; protected set; }

        public List<ISpriteBase> Children;
        
        public Group()
        {
            
        }


        public void Load()
        {
            foreach (ISpriteBase s in Children)
                s.Load();

            Loaded = true;
        }

        public void Dispose()
        {
            Clear();
        }

        public void Add(ISpriteBase item)
        {
            if (item == null)
                return;

            if (Loaded && !item.Loaded)
                item.Load();
            
            Children.Add(item);
        }

        public void Clear()
        {
            foreach (ISpriteBase s in Children)
                s.Dispose();
            
            Children.Clear();
        }

        public bool Remove(ISpriteBase item)
        {
            item?.Dispose();
            return Children.Remove(item);
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            
        }

        # region rest of ICollection implemetnation
        public IEnumerator<ISpriteBase> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(ISpriteBase item)
        {
            return Children.Contains(item);
        }

        public void CopyTo(ISpriteBase[] array, int arrayIndex)
        {
            Children.CopyTo(array, arrayIndex);
        }

        public int Count => Children.Count;
        public bool IsReadOnly => false;
        
        #endregion
    }
}
