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
                // positioning
                var pos = Matrix.GetTranslationFromMat(Transformations);
                var big = Matrix.GetScaleFromMat(Transformations);
                var rot = 180/Math.PI * Matrix.GetRotationFromMat(Transformations);
                
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
                    SDL_RenderCopyEx(renderer, CurrentTexture.Handle, IntPtr.Zero, ref destRect, Rotation, ref _origin,
                        (SDL_RendererFlip)Flip);
                }
            }
        }
    }
}