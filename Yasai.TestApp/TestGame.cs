using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Audio;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Stores;
using Rectangle = Yasai.Graphics.Rectangle;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Container c;
        private Drawable b;

        private int n;
        private readonly SpriteText text;

        sealed class TestSprite : Sprite
        {
            private string k;
            
            public TestSprite(Texture texture, string k) : base(texture)
            {
                Size = new Vector2(100);
                this.k = k;
            }

            public override bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
            {
                Console.WriteLine("some of this wangs " + k);
                return false;
            }
        }
        
        public TestGame()
        {
            TextureStore tstore = new TextureStore();
            tstore.LoadResource("school.png", "tex");
            Texture t = tstore.GetResource("tex");

            TextureStore bruh = new TextureStore();
            bruh.LoadSpritesheet("school.png", new Dictionary<string, Rectangle>()
            {
                //{ "kaos", new Rectangle(348, 495, 469, 327) }
                { "kaos", new Rectangle(180, 150, 51, 41) }
            });
            Texture kaos = bruh.GetResource("kaos");

            FontStore fonts = new FontStore();
            fonts.LoadResource("font.fnt");
            SpriteFont font = fonts.GetResource("font");

            AudioStore audioStore = new AudioStore();
            audioStore.LoadResource("nami.mp3");

            Channel ch = new Channel(audioStore.GetResource("nami"));
            ch.Play();
            
            Children = new IDrawable[]
            {
                c = new Container
                {
                    Anchor = Anchor.Center, 
                    Origin = Anchor.Center, 
                    Size = new Vector2(400),
                    Colour = Color.Red,
                    Fill = true,
                    Items = new IDrawable[]
                    {
                        b = new TestSprite(t, "left")
                        {
                            Anchor = Anchor.Center,
                            Origin = Anchor.Right,
                            X = 30,
                        },
                        new TestSprite(t, "right")
                        {
                            Anchor = Anchor.Center,
                            Origin = Anchor.Left,
                            X = -30
                        },
                    }
                },
                new Sprite (kaos)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Size = new Vector2(400),
                },
                text = new SpriteText ("thic hwan small peepee え", font)
                {
                    Position = new Vector2(10, 300)
                }
            };
            
            c.MousePressEvent += (_, _) => Console.WriteLine("wangs");
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            c.X += 0.1f;
        }
    }
}