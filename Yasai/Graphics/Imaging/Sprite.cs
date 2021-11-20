using System;
using System.Numerics;
using System.Drawing;
using Yasai.Debug.Logging;
using Yasai.Extensions;
using Yasai.Resources;
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

        private Vector2 size = new Vector2(100);
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


        private float alpha = 1;
        public override float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                if (alpha > 1)
                {
                    alpha = 1;
                    GameBase.YasaiLogger.LogWarning("alpha was larger than 1, ensure that alpha remains a number between 0 and 1");
                }
            }
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
            
            if (SDL_QueryTexture(CurrentTexture.Handle, out _, out _, out _, out _) != 0)
                throw new Exception(SDL_GetError());
            
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
                SDL_Rect destRect;
                destRect.x = (int) Position.X;
                destRect.y = (int) Position.Y;
                destRect.w = (int) Size.X;
                destRect.h = (int) Size.Y;

                SDL_Point _origin = Offset.ToSdlPoint();

                // update colour and alpha
                var alphares 
                    = SDL_SetTextureColorMod(CurrentTexture.Handle, (colour.R), (colour.G), (colour.B));
                
                var colres 
                    = SDL_SetTextureAlphaMod(CurrentTexture.Handle, (byte)(alpha * 255));
                
                if (alphares != 0 || colres != 0)
                    throw new Exception(SDL_GetError());
            
                // drawing
                if (Visible && Enabled)
                    SDL_RenderCopyEx(renderer, CurrentTexture.Handle, IntPtr.Zero, ref destRect, Rotation, ref _origin,
                        (SDL_RendererFlip)Flip);
            }
        }
    }
}