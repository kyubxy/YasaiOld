using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Primitives
{
    public abstract class Primitive : IDrawable, ISimpleGeometry
    {
       public virtual bool Enabled { get; set; } = true;
       public virtual bool Visible { get; set; } = true;
       public virtual Color4 Colour { get; set; } = Color4.White;
       public virtual Vector2 Position { get; set; } = new Vector2(400);
       public virtual Vector2 Size { get; set; } = new Vector2(200);
       public virtual bool Fill { get; set; } = true;
       public virtual bool Loaded => true;
       
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

       public void Load(ContentCache cache)
       {
       }
       
       public virtual void Draw(IntPtr renderer)
       {
           SDL.SDL_SetRenderDrawColor(renderer, (byte) (Colour.R * 255), (byte) (Colour.G * 255),
               (byte) (Colour.B * 255), 255);
       }
    }
}