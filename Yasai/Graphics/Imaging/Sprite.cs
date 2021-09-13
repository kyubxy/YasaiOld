using System;
using OpenTK.Mathematics;
using SDL2;
using Yasai.Extensions;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public enum Flip
    {
        None = SDL.SDL_RendererFlip.SDL_FLIP_NONE,
        Horizontal = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL,
        Vertical = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL,
    }
    
    public class Sprite : Drawable
    {
        public Texture CurrentTexture { get; private set; }
        
        public override bool Loaded => CurrentTexture != null && CurrentTexture.Handle != IntPtr.Zero;
        
        private readonly string path;

        private Vector2 size = Vector2.Zero; 
        public override Vector2 Size
        {
            get => size;
            set
            {
                size = value;
                
                if (!Loaded) return;
                Vector2 img = CurrentTexture.Size;
                Origin = new Vector2(Size.X / 2, Size.Y / 2);
            }
        }

        public Flip Flip = Flip.None;
        
        private Color4 colour = Color4.White;
        public override Color4 Colour
        {
            get => colour;
            set
            {
                colour = value;
                if (colour.A != 1)
                    Console.WriteLine("Setting alpha through Color4 is not supported, use Opacity instead");
            }
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
                    Console.WriteLine("alpha was larger than 1, ensure that alpha remains a number between 0 and 1");
                }
            }
        }

        public Sprite(string path)
        {
            this.path = path;
        }

        public Sprite(Texture tex)
        {
            CurrentTexture = tex;
            if (SDL.SDL_QueryTexture(CurrentTexture.Handle, out _, out _, out _, out _) != 0)
                throw new Exception(SDL.SDL_GetError());
            
            Size = Size == Vector2.Zero ? CurrentTexture.Size : Size;
            Origin = new Vector2(Size.X / 2, Size.Y / 2);
        }
        
        public override void Load(ContentStore cs)
        {
            base.Load(cs);
            
            if (cs == null)
                throw new NullReferenceException("the content store was null");
            
            if (!Loaded)
                CurrentTexture = cs.GetResource<Texture>(path);

            Size = Size == Vector2.Zero ? CurrentTexture.Size : Size;
            Origin = new Vector2(Size.X / 2, Size.Y / 2);
        }
        
        public override void Draw(IntPtr renderer)
        {
            base.Draw(renderer);
            
            if (CurrentTexture != null)
            {
                // positioning
                SDL.SDL_Rect destRect;
                destRect.x = (int) Position.X;
                destRect.y = (int) Position.Y;
                destRect.w = (int) Size.X;
                destRect.h = (int) Size.Y;

                SDL.SDL_Point origin = Origin.ToSdlPoint();

                // update colour and alpha
                var alphares 
                    = SDL.SDL_SetTextureColorMod(CurrentTexture.Handle, (byte) (colour.R * 255), (byte) (colour.G * 255), (byte) (colour.B * 255));
                
                var colres 
                    = SDL.SDL_SetTextureAlphaMod(CurrentTexture.Handle, (byte)(alpha * 255));
                
                if (alphares != 0 || colres != 0)
                    throw new Exception(SDL.SDL_GetError());
            
                // drawing
                if (Visible && Enabled)
                    SDL.SDL_RenderCopyEx(renderer, CurrentTexture.Handle, IntPtr.Zero, ref destRect, Rotation, ref origin,
                        (SDL.SDL_RendererFlip)Flip);
            }
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            // TODO: implement a copy constructor
            // https://stackoverflow.com/questions/25738096/c-sdl2-error-when-trying-to-render-sdl-texture-invalid-texture
            /*
            if (Loaded)
                SDL.SDL_DestroyTexture(CurrentTexture.Handle);
                */
        }
        
        
        // TODO: animations (later)
        public void NextCostume()
        { }

        public void ToCostume(int n)
        { }
    }
}