using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using Yasai.Graphics.Primitives;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics.Layout.Groups
{
    public class Group : Drawable, ICollection<IDrawable>, IMouseListener, IKeyListener
    {
        private List<IDrawable> _children;
        private ContentStore _contentStore;

        private PrimitiveBox _primitiveBox;

        private Vector2 position;
        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                _primitiveBox.Position = value;
            }
        }

        private Vector2 size;
        public override Vector2 Size
        {
            get => size;
            set
            {
                size = value;
                _primitiveBox.Size = value;
            }
        }

        private bool fill;
        public bool Fill
        {
            get => fill;
            set
            {
                fill = value;
                _primitiveBox.Enabled = value;
            }
        }

        public override bool Loaded => _children.All(x => x.Loaded) && _contentStore != null;

        public Group(List<IDrawable> children)
        {
            _children = children;
            _primitiveBox = new PrimitiveBox();
            Fill = false;
        }

        public Group() : this (new List<IDrawable>())
        { }

        public Group(IDrawable[] children) : this(children.ToList())
        {
        }

        public override void Load(ContentStore cs)
        {
            _primitiveBox.Load(cs);
            foreach (IDrawable s in _children)
                s.Load(cs);
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

        public void AddAll(IDrawable[] array)
        {
            foreach (IDrawable d in array)
                Add(d);
        }

        public void Clear()
        {
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
            if (Visible && Enabled)
            {
                _primitiveBox.Draw(renderer);
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

        // i also like good and maintainable code
        
        public bool IgnoreHierachy { get; }
        public virtual void KeyUp(KeyCode key)
        {
            int i = 0;
            foreach (var k in _children)
            {
                IKeyListener listener = k as IKeyListener;
            
                if (listener == null) 
                    continue;

                if (i == _children.Count - 1 || listener.IgnoreHierachy)
                    listener.KeyUp(key);
            
                i++;
            }
        }

        public virtual void KeyDown(KeyCode key)
        {
            int ii = 0;
            foreach (var k in _children)
            {
                IKeyListener listener = k as IKeyListener;
            
                if (listener == null) 
                    continue;
            
                if (ii == _children.Count - 1 || listener.IgnoreHierachy)
                    listener.KeyDown(key);
            
                ii++;
            }
        }

        public virtual void MouseDown(MouseArgs args)
        {
            MouseButton button = args.Button;
            Vector2 position = args.Position;
            
            int iii = 0;
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;
            
                if (listener == null)
                    continue;
            
                if (iii == _children.Count - 1 || listener.IgnoreHierachy)
                    listener.MouseDown(new MouseArgs(button, position));
                
                iii++;
            }
        }

        public virtual void MouseUp(MouseArgs args)
        {
            MouseButton button = args.Button;
            Vector2 position = args.Position;
            
            int iv = 0;
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;

                if (listener == null)
                    continue;

                if (iv == _children.Count - 1 || listener.IgnoreHierachy)
                    listener.MouseUp(new MouseArgs(button, position));
                
                iv++;
            }
        }

        public virtual void MouseMotion(MouseArgs args)
        {
            MouseButton button = args.Button;
            Vector2 position = args.Position;
            
            int v = 0;
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;
            
                if (listener == null)
                    continue;
            
                if (v == _children.Count - 1 || listener.IgnoreHierachy)
                    listener.MouseMotion(new MouseArgs(position));
                
                v++;
            }
        }
    }
}
