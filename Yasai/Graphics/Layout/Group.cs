using System;
using System.Collections;
using System.Collections.Generic;
using Yasai.Resources;

namespace Yasai.Graphics.Layout
{
    public class Group : Drawable, ICollection<IDrawable>
    {
        private List<IDrawable> _children;
        private ContentStore _contentStore;
        
        public Group(List<IDrawable> children)
        {
            _children = children;
        }

        public Group()
        {
            _children = new List<IDrawable>();
        }

        public override void Load(ContentStore cs)
        {
            foreach (IDrawable s in _children)
                s.Load(cs);

            Loaded = true;
        }

        public override void Dispose()
        {
            Clear();
        }

        public void Add(IDrawable item)
        {
            if (item == null)
                return;

            if (Loaded && !item.Loaded)
                item.Load(_contentStore);
            
            _children.Add(item);
        }

        public void Clear()
        {
            foreach (IDrawable s in _children)
                s.Dispose();
            
            _children.Clear();
        }

        public bool Remove(IDrawable item)
        {
            item?.Dispose();
            return _children.Remove(item);
        }

        public override void Update()
        {
            if (Enabled)
            {
                foreach (IDrawable s in _children)
                    s.Update();
            }
        }

        public override void Draw(IntPtr renderer)
        {
            if (Visible)
            {
                foreach (IDrawable s in _children)
                    s.Draw(renderer);
            }
        }

        # region rest of ICollection implemetnation
        public IEnumerator<IDrawable> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(IDrawable item)
        {
            return _children.Contains(item);
        }

        public void CopyTo(IDrawable[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public int Count => _children.Count;
        public bool IsReadOnly => false;
        
        #endregion
    }
}
