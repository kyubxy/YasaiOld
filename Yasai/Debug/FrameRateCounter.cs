using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;

using static SDL2.SDL;

namespace Yasai.Debug
{
    public class FrameRateCounter : Widget
    {
        private Anchor anchor;
        private SpriteText text;
        private PrimitiveBox back;

        public ulong StartCount;
        public ulong EndCount;
        
        public float Elapsed => ((EndCount - StartCount) / (float)SDL_GetPerformanceFrequency());
        public float FPS => 1 / Elapsed;
        
        public FrameRateCounter(Anchor anchor = Anchor.TopLeft)
        {
            if (anchor != Anchor.TopLeft)
            {
                this.anchor = Anchor.TopLeft;
                Console.WriteLine("other anchors are not supported yet");
            } 
        }
        
        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            AddAll(new IDrawable[]
            {
                back = new PrimitiveBox()
                {
                    Position = new Vector2(10),
                    Size = new Vector2(80,25),
                    Fill = true,
                    Colour = BACKGROUND_COLOUR
                },
                text = new SpriteText("FPS", Constants.tinyFont)
                {
                    Position = new Vector2(15),
                    Colour = Color.Black
                }
            });
        }

        public override void Update()
        {
            base.Update();
            text.Text = $"{Math.Round(FPS)} FPS";
            if (FPS > 55)
                back.Colour = Color.LightGreen;
            else if (FPS > 50 && FPS < 55)
                back.Colour = Color.GreenYellow;
            else if (FPS > 30 && FPS < 50)
                back.Colour = Color.Yellow;
            else if (FPS > 10 && FPS < 30)
                back.Colour = Color.Orange;
            else if (FPS < 10)
                back.Colour = Color.Red;
        }
    }
}