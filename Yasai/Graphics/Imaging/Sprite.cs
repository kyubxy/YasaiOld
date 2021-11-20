using System;
using System.Numerics;
using System.Drawing;
using Yasai.Extensions;
using Yasai.Maths;
using Yasai.Structures.DI;
using static SDL2.SDL;

namespace Yasai.Graphics.Imaging
{
    public enum Flip
    {
        None = SDL_RendererFlip.SDL_FLIP_NONE,
        Horizontal = SDL_RendererFlip.SDL_FLIP_HORIZONTAL,
        Vertical = SDL_RendererFlip.SDL_FLIP_VERTICAL,
    }
    
    public class Sprite : Drawable
    {
        public Texture CurrentTexture { get; protected set; }
        
        public override bool Loaded => CurrentTexture?.Handle != IntPtr.Zero && base.Loaded;

        private Vector2 size = new (100);
        public override Vector2 Size
        {
            get => size;
            set
            {
                size = value;
                
                if (!Loaded) return;
                Offset = new Vector2(Size.X / 2, Size.Y / 2);
            }
        }

        public Flip Flip = Flip.None;
        
        private Color colour = Color.White;
        public override Color Colour
        {
            get => colour;
            set => colour = value;
        }

        // VERY temporary
        private bool setOrigin;
        private Vector2 origin;
        public override Vector2 Offset
        {
            get => origin;
            set
            {
                origin = value;
                setOrigin = true;
            }
        }

        public Sprite() { }

        public Sprite(Texture tex)
        {
            CurrentTexture = tex;

            int w, h;
            
            if (SDL_QueryTexture(CurrentTexture.Handle, out _, out _, out w, out h) != 0)
                throw new Exception(SDL_GetError());
            
            Size = new Vector2(w, h);
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            if (!setOrigin)
                CenterToCurrentTex();
        }

        protected void CenterToCurrentTex()
        {
            Size = Size == Vector2.Zero ? CurrentTexture.Size : Size;
            Offset = new Vector2(Size.X / 2, Size.Y / 2);
        }
        
        public override void Draw(IntPtr renderer)
        {
            base.Draw(renderer);
            
            if (CurrentTexture != null)
            {
                // very important lesson in linear algebra: the point of a transformation matrix is
                // to take individual vertices of a unit square and pre-operate a matrix on each to
                // find the coordinates for the new points which are then used to draw the shape.
                
                // for now, we'll use some hacks to get numbers which abide by the SDL way of doing things
                
                //get the coordinate of the top left point at (0,1) on the unit square
                var pos = Matrix.DotMultiply(Transformations, new Vector3(0, 1, 1));
                
                // use the inverse tangent of two horizontally adjacent points on the unit square
                var p0 = Matrix.DotMultiply(Transformations, new Vector3(0, 0, 1));
                var p1 = Matrix.DotMultiply(Transformations, new Vector3(0, 1, 1));
                var rot = Math.Atan((p1.Y - p0.Y) / (p1.X - p0.X)) * 180 / Math.PI + 90;
                
                // get the difference between the x and y coords and use those as the sizes
                // TODO:
                
                SDL_Rect destRect;
                destRect.x = (int) pos.X;
                destRect.y = (int) pos.Y;
                destRect.w = (int) Size.X;
                destRect.h = (int) Size.Y;

                SDL_Point _origin = Offset.ToSdlPoint();

                // update colour and alpha
                var alphares 
                    = SDL_SetTextureColorMod(CurrentTexture.Handle, (colour.R), (colour.G), (colour.B));

                    
                var colres 
                    = SDL_SetTextureAlphaMod(CurrentTexture.Handle, (byte)(Alpha * 255));
                
                if (alphares != 0 || colres != 0)
                    throw new Exception(SDL_GetError());
            
                // drawing
                if (Visible && Enabled)
                {
                    SDL_SetTextureBlendMode(CurrentTexture.Handle, SDL_BlendMode.SDL_BLENDMODE_BLEND);
                    SDL_RenderCopyEx(renderer, CurrentTexture.Handle, IntPtr.Zero, ref destRect, rot, ref _origin,
                        (SDL_RendererFlip)Flip);
                }
            }
        }
    }
}