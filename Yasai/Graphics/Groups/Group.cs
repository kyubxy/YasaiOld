using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Yasai.Debug;
using Yasai.Graphics.Primitives;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Groups
{
    public class Group : Drawable, IGroup, ICollection<IDrawable>
    {
        private readonly List<IDrawable> children;
        
        public virtual bool IgnoreHierarchy { get; set; } = true;

        private readonly Primitive box;

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

        #region constructors
        public Group(List<IDrawable> children)
        {
            this.children = children;
            box = new PrimitiveBox();
            Fill = false;
        }

        public Group() : this (new List<IDrawable>())
        { }

        public Group(IDrawable[] children) : this(children.ToList())
        { }
        #endregion

        #region lifespan

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            
            box.Load(dependencies);
            foreach (IDrawable s in children)
                s.Load(dependencies);
        }

        public override void Update()
        {
            base.Update();
            
            if (Enabled)
                foreach (IDrawable s in children)
                    s.Update();
        }

        public override void Draw(IntPtr renderer)
        {
            base.Draw(renderer);
            
            if (Visible && Enabled)
            {
                if (Fill)
                    box.Draw(renderer);

                foreach (IDrawable s in children)
                {
                    if (!(s is Widget))
                        s.Draw(renderer);
                }
            }
        }
        #endregion
        
        #region collection stuff
        public void Add(IDrawable item)
        {
            if (item == null)
                return;

            if (Loaded && !item.Loaded)
                item.Load(Dependencies);
            
            children.Add(item);
        }

        public void AddAll(IDrawable[] array)
        {
            foreach (IDrawable d in array)
                Add(d);
        }

        public void Clear()
        {
            children.Clear();
        }

        public bool Remove(IDrawable item)
        {
            if (item == null) return false;
            return children.Remove(item);
        }
        #endregion
        
        # region rest of ICollection implementation
        public IEnumerator<IDrawable> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(IDrawable item)
        {
            return children.Contains(item);
        }

        public void CopyTo(IDrawable[] array, int arrayIndex)
        {
            children.CopyTo(array, arrayIndex);
        }

        public int Count => children.Count;
        public bool IsReadOnly => false;
        
        #endregion

        public override void KeyDown(KeyArgs key)
        {
            foreach (IDrawable handler in children)
            {
                if (!handler.Enabled)
                    break;
                
                handler.KeyDown(key);
            }
        }
        
        public override void KeyUp(KeyArgs key)
        {
            foreach (IDrawable handler in children)
            {
                if (!handler.Enabled)
                    break;
                
                handler.KeyUp(key);
            }
        }

        public override void MouseDown(MouseArgs args)
        {
            foreach (IDrawable handler in children)
            {
                if (!handler.Enabled)
                    break;
                
                handler.MouseDown(args);
            }
            
            base.MouseDown(args);
        }
        
        public override void MouseUp(MouseArgs args)
        {
            foreach (IDrawable handler in children)
            {
                if (!handler.Enabled)
                    break;
                
                handler.MouseUp(args);
            }
            
            base.MouseUp(args);
        }
        
        public override void MouseMotion(MouseArgs args)
        {
            foreach (IDrawable handler in children)
            {
                if (!handler.Enabled)
                    break;
                
                handler.MouseMotion(args);
            }
            
            base.MouseMotion(args);
        }
    }
}
