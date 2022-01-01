using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Text;
using Yasai.Resources;
using Yasai.Resources.Stores;

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
            bruh.LoadSpritesheet("kaos.jpg", new SpritesheetData(new Dictionary<string, SpritesheetData.Tile>()
            {
                { "kaos", new SpritesheetData.Tile(348, 495, 469, 327) }
            }));
            Texture kaos = bruh.GetResource("kaos");

            // content stores should stop requiring dependencies
            FontStore fonts = new FontStore();
            fonts.LoadResource("font.fnt");
            SpriteFont font = fonts.GetResource("font");
            
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
                text = new SpriteText ("thick hwan small peepee", font)
                {
                    Position = new Vector2(10, 300)
                }
            };
            
            c.MousePressEvent += (_, _) => Console.WriteLine("wangs");
        }
    }
}