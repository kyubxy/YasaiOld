using System;
using System.Drawing;
using System.Numerics;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;
using Yasai.Structures;
using Yasai.Structures.DI;
using static SDL2.SDL;

namespace Yasai.Graphics.Primitives
{
    public abstract class Primitive : IDrawable, ISimpleGeometry
    {
        public virtual bool Enabled { get; set; } = true;
        public virtual bool Visible { get; set; } = true;
        public virtual Color Colour { get; set; } = Color.White;
        public virtual Vector2 Position { get; set; } = new Vector2(400);
        public virtual Vector2 Size { get; set; } = new Vector2(200);
        public virtual bool Fill { get; set; } = true;
        public virtual bool Loaded => Dependencies != null;

        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }
        
        public float Width
        {
            get => Size.X;
            set => Size = new Vector2(value, Size.Y);
        }
        public float Height
        {
            get => Size.Y;
            set => Size = new Vector2(Size.X, value);
        }
        
        public virtual void Update()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void Load(DependencyContainer dependencies)
        {
        }

        public virtual void LoadComplete()
        {
        }

        public virtual void Draw(IntPtr renderer)
        {
            SDL_SetRenderDrawColor(renderer, Colour.R, Colour.G, Colour.B, 255);
        }

        public bool Resolved => Dependencies != null;
        public DependencyContainer Dependencies { get; set; }

        public virtual void MouseDown(MouseArgs args) { }
        public virtual void MouseUp(MouseArgs args) { }
        public virtual void MouseMotion(MouseArgs args) { }
        public virtual void KeyUp(KeyArgs key)  { }
        public virtual void KeyDown(KeyArgs key) { }
    }
}