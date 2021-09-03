using System;
using System.Collections.Generic;
using SDL2;
using Yasai.Resources;

namespace Yasai.Graphics.Imaging
{
    public class Sprite : Drawable
    {
        // TODO: animation support
        public Texture CurrentTexture;
        //public List<Texture> Costumes;
        
        private readonly string path;
        
        public Sprite(string path)
        {
            this.path = path;
        }

        public Sprite(Texture tex)
        {
            CurrentTexture = tex;
            Loaded = true;
        }
        
        public override void Load(ContentStore cs)
        {
            if (cs == null)
                throw new NullReferenceException("the content store was null");
            
            if (!Loaded)
                CurrentTexture = cs.GetResource<Texture>(path);
        }
        
        public override void Draw(IntPtr renderer)
        {
            if (CurrentTexture != null)
            {
                SDL.SDL_Rect destRect;
                destRect.x = (int) Position.X;
                destRect.y = (int) Position.Y;
                destRect.w = (int) Size.X;
                destRect.h = (int) Size.Y;

                SDL.SDL_RenderCopy(renderer, CurrentTexture.Handle, IntPtr.Zero, ref destRect);
            }
        }

        public override void Dispose()
        {
            SDL.SDL_DestroyTexture(CurrentTexture.Handle);
        }
        
        
        // TODO:
        public void NextCostume()
        { }

        public void ToCostume(int n)
        { }
    }
}