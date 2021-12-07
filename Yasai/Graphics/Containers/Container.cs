using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shapes;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Containers
{
    public class Container : Drawable, ICollection<IDrawable>
    {
        private readonly List<IDrawable> children;

        private readonly Box box;

        private DependencyContainer dependencies;

        public IDrawable[] Items { init => AddAll(value); }

        public bool Fill { get; set; } 
        
        private Color colour;
        public override Color Colour
        {
            get => colour;
            set
            {
                box.Colour = value;
                colour = value;
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
        
        #region constructors
        public Container(List<IDrawable> children)
        {
            this.children = new List<IDrawable>();
            AddAll(children.ToArray());
            
            box = new Box()
            {
                Parent = this
            };
        }

        public Container() : this (new List<IDrawable>())
        { }

        public Container(IDrawable[] children) : this(children.ToList())
        { }
        
        #endregion

        #region lifespan

        public override void Load(DependencyContainer dep)
        {
            base.Load(dep);

            dependencies = dep;
            
            box.Load(dep);
            foreach (IDrawable s in children)
                s.Load(dep);

            Loaded = true;
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            
            if (Enabled)
                foreach (IDrawable s in children)
                    s.Update(args);
        }

        // tightly coupling to this one until there's a good enough reason
        // to decouple this behaviour
        public void Draw()
        {
           if (Fill) 
               drawPrimitive(box);
            
           foreach (IDrawable drawable in children)
               if (drawable is Container c)
                   c.Draw();
               else if (drawable is Primitive p)
                   drawPrimitive(p);
        }
        
        /// <summary>
        /// Render a single <see cref="Primitive"/> to the screen
        /// </summary>
        /// <param name="primitive"></param>
        private void drawPrimitive(Primitive primitive)
        {
           //if (!primitive.Enabled || !primitive.Visible)
           //    return;
            
            var shader = primitive.Shader;
            
            shader.Use();
            primitive.Use();
            
            // assuming the drawable uses a vertex shader with model and projection matrices
            shader.SetMatrix4("model", primitive.ModelTransforms);
            shader.SetMatrix4("projection", GameBase.Projection);
            
            GL.DrawElements(PrimitiveType.Triangles, primitive.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        
        #endregion
        
        #region collection stuff
        public void Add(IDrawable item)
        {
            if (item == null)
                return;

            item.Parent = this;

            if (Loaded && !item.Loaded)
                item.Load(dependencies);
            
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
    }
}