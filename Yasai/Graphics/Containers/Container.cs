using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        private IDrawable[] items;
        public IDrawable[] Items
        {
            get => items;
            set
            {
                items = value;
                Clear();
                AddAll(items);
            }
        }

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

        public override void Load(DependencyContainer container)
        {
            base.Load(container);

            dependencies = container;
            
            box.Load(container);
            foreach (IDrawable s in children)
                s.Load(container);

            Loaded = true;
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            
            if (Enabled)
                foreach (IDrawable s in children)
                    s.Update(args);
        }

        public override void Draw()
        {
           if (Fill) 
               drawPrimitive(box);
            
           foreach (IDrawable drawable in children)
               if (drawable is Primitive p)
                   drawPrimitive(p);
               else if (drawable is Drawable d)
                   d.Draw();
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
            
            primitive.Draw();
            shader.Use();
            
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
            foreach (var c in children)
                c.Dispose();
            
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
        
        #region input
        
            
        bool pointInDrawable(Vector2 point, IDrawable d)
            => point.X >= d.AbsoluteTransform.Position.X && point.X <= d.AbsoluteTransform.Position.X + d.Size.X && point.Y >= d.AbsoluteTransform.Position.Y &&
               point.Y <= d.AbsoluteTransform.Position.Y + d.Size.Y;
        
        // to avoid managing a reversed version of the children list, the input functions will iterate the children list in reverse
            
        // mouse

        public override void GlobalMouseMove(MouseMoveEventArgs args)
        {
            base.GlobalMouseMove(args);
            foreach (var child in children)
                child.GlobalMouseMove(args);
        }

        public override void GlobalMousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
            base.GlobalMousePress(position, buttonArgs);
            foreach (var child in children)
                child.GlobalMousePress(position, buttonArgs);
        }


        public override bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
            for (int i = 0; i < children.Count; i++)
            {
                IDrawable d = children[children.Count - i - 1];
                
                if (!d.Enabled)
                    continue;

                if (!pointInDrawable(position, d))
                    continue;

                var result = d.MousePress(position, buttonArgs);

                if (!result)
                    return false;
            }

            return base.MousePress(position, buttonArgs);
        }
        
        public override bool MouseMove(MouseMoveEventArgs args)
        {
            for (int i = 0; i < children.Count; i++)
            {
                IDrawable d = children[children.Count - i - 1];
                if (!d.Enabled)
                    continue;
                
                if (!pointInDrawable(args.Position, d))
                    continue;

                var result = d.MouseMove(args);

                if (!result)
                    return false;
            }

            return base.MouseMove(args);
        }

        public override bool MouseScroll(Vector2 position, MouseWheelEventArgs args)
        {
            for (int i = 0; i < children.Count; i++)
            {
                IDrawable d = children[children.Count - i - 1];
                if (!d.Enabled)
                    continue;
                
                if (!pointInDrawable(Position, d))
                    continue;

                var result = d.MouseScroll(position, args);

                if (!result)
                    return false;
            }

            return base.MouseScroll(position, args);
        }

        // keyboard
        
        // keyboard events for all drawables in the scenegraph are processed
        // this might affect performance
        
        public override void KeyDown(KeyboardKeyEventArgs args)
        {
            base.KeyDown(args);
            
            for (int i = 0; i < children.Count; i++)
            {
                IDrawable d = children[children.Count - i - 1];
                
                if (d.Enabled)
                    d.KeyDown(args);
            }
        }

        public override void KeyUp(KeyboardKeyEventArgs args)
        {
            base.KeyUp(args);

            for (int i = 0; i < children.Count; i++)
            {
                IDrawable d = children[children.Count - i - 1];
                
                if (d.Enabled)
                    d.KeyUp(args);
            }
        }
        
        #endregion
    }
}