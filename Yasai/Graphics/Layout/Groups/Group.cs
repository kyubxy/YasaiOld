using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Yasai.Graphics.Primitives;
using Yasai.Input;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Graphics.Layout.Groups
{
    public class Group : Drawable, IGroup, ICollection<IDrawable>
    {
        private List<IDrawable> _children;
        private ContentCache _contentCache;

        public virtual bool IgnoreHierachy { get; set; } = true;

        private Primitive box;

        private Vector2 position;
        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                box.Position = value;
            }
        }

        private Vector2 size;
        public override Vector2 Size
        {
            get => size;
            set
            {
                size = value;
                box.Size = value;
            }
        }

        private bool fill;
        public bool Fill
        {
            get => fill;
            set
            {
                fill = value;
                box.Enabled = value;
            }
        }
        
        

        public override bool Loaded => _children.All(x => x.Loaded) && _contentCache != null;

        #region constructors
        public Group(List<IDrawable> children)
        {
            _children = children;
            box = new PrimitiveBox();
            Fill = false;
        }

        public Group() : this (new List<IDrawable>())
        { }

        public Group(IDrawable[] children) : this(children.ToList())
        { }
        #endregion

        #region lifespan

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
        }

        public override void Load(ContentCache cache)
        {
            base.Load(cache);
            
            // NOTE: might be kind of risky..
            foreach (IDrawable s in _children)
                s.Start(cache);
            
            box.Load(cache);
            foreach (IDrawable s in _children)
                s.Load(cache);
            _contentCache = cache;
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
                if (Fill)
                    box.Draw(renderer);
                
                foreach (IDrawable s in _children)
                    s.Draw(renderer);
            }
        }
        #endregion
        
        #region collection stuff
        public void Add(IDrawable item)
        {
            if (item == null)
                return;

            if (Loaded && !item.Loaded)
                item.Load(_contentCache);
            
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
        #endregion
        
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
        
        public virtual void KeyUp(KeyCode key)
        {
            if (!Enabled)
                return;
            
            foreach (var k in _children)
            {
                IKeyListener listener = k as IKeyListener;
                if (listener == null) 
                    continue;

                if (isActiveInStack((listener)))
                    listener.KeyUp((key));
            }
        }

        public virtual void KeyDown(KeyCode key)
        {
            if (!Enabled)
                return;
            
            foreach (var k in _children)
            {
                IKeyListener listener = k as IKeyListener;
                if (listener == null) 
                    continue;
            
                if (isActiveInStack(listener))
                    listener.KeyDown(key);
            }
        }

        public virtual void MouseDown(MouseArgs args)
        {
            if (!Enabled)
                return;
            
            MouseButton button = args.Button;
            Vector2 mousepos = args.Position;
            
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;
                if (listener == null)
                    continue;
                
                if (isActiveInStack(listener))
                    listener.MouseDown(args);
            }
        }

        public virtual void MouseUp(MouseArgs args)
        {
            if (!Enabled)
                return;
            
            MouseButton button = args.Button;
            Vector2 mousepos = args.Position;
            
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;
                if (listener == null)
                    continue;

                if (isActiveInStack(listener))
                    listener.MouseUp(args);
            }
        }

        public virtual void MouseMotion(MouseArgs args)
        {
            if (!Enabled)
                return;
            
            MouseButton button = args.Button;
            Vector2 mousepos = args.Position;
            
            foreach (var k in _children)
            {
                IMouseListener listener = k as IMouseListener;
                if (listener == null)
                    continue;

                if (isActiveInStack(listener))
                    listener.MouseMotion(args);
            }
        }

        bool isActiveInStack(IListener x)
        {
            if (x.IgnoreHierachy && x.Enabled)
                return true;

            Stack<IDrawable> reversed = new Stack<IDrawable>(this);
            while (reversed.Count > 0)
            {
                var h = (IListener)reversed.Pop();
                if (h.Enabled && !h.IgnoreHierachy)
                {
                    return h == x;
                }

                if (h == x)
                    return true;
            }
            return false;
        }
    }
}
