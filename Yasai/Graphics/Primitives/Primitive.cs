using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Primitives
{
    public abstract class Primitive : IDrawable, ISimpleGeometry
    {
       public virtual Color4 OutlineColour { get; set; }
       public virtual bool Outline { get; set; } = true;
       public virtual bool Enabled { get; set; } = true;
       public virtual bool Visible { get; set; } = true;
       public virtual float Alpha { get; set; } = 1;
       public virtual Color4 Colour { get; set; } = Color4.White;
       public virtual float Thickness { get; set; } = 1;
       public virtual bool Loaded => true;
       public virtual Vector2 Position { get; set; } = new Vector2(400);
       public virtual Vector2 Size { get; set; } = new Vector2(200);
       
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

       public void Load(ContentStore cs)
       {
       }
       
       public virtual void Draw(IntPtr renderer)
       {
           SDL.SDL_RenderSetScale(renderer, Thickness, Thickness);

           SDL.SDL_SetRenderDrawColor(renderer,
               (byte) (OutlineColour.R * 255), (byte) (OutlineColour.G * 255), (byte) (OutlineColour.B * 255),
               (byte) (Outline ? Alpha * 255 : 0));
       }
    }
}