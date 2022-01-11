using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Yasai.Audio;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Screens;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

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

        private ScreenManager sm;
        
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


            AudioStore audioStore = new AudioStore();
            audioStore.LoadResource(@"sakura_02.wav");
            Channel ch = new Channel(audioStore.GetResource("sakura_02"));
            ch.Play();

            Children = new IDrawable[]
            {
                sm = new ScreenManager(new Screen1())
            };
            
            sm.KeyDownEvent += args =>
            {
                if (args.Key == Keys.Space)
                {
                    Console.WriteLine("space");
                    sm.PushScreen(new Screen2());
                } 
                else if (args.Key == Keys.N)
                {
                    Console.WriteLine("n");
                    sm.PushScreen(new Screen1());
                }
            };
            
            FontStore fonts = new FontStore();
            fonts.LoadResource("font.fnt", SpriteFont.Normal);
            Dependencies.Register<FontStore>(fonts);

            TextureStore textures = new TextureStore();
            textures.LoadResource("kaos.jpg");
            Dependencies.Register<TextureStore>(textures);
        }
    }

    class Screen1 : Screen
    {
        public override void Load(DependencyContainer container)
        {
            base.Load(container);
            
            var fonts = container.Resolve<FontStore>();

            Items = new IDrawable[]
            {
                new SpriteText("screen 1", fonts.GetResource(SpriteFont.Normal))
                {
                    Anchor = Anchor.Center 
                }
            };
        }
    }

    class Screen2 : Screen
    {
        private Texture tex;
        
        public override void Load(DependencyContainer container)
        {
            base.Load(container);

            var texStore = container.Resolve<TextureStore>();

            tex = texStore.GetResource("kaos");
            
            var fonts = container.Resolve<FontStore>();

            Items = new IDrawable[]
            {
                new SpriteText("screen 2", fonts.GetResource(SpriteFont.Normal))
                {
                    Anchor = Anchor.Center 
                },
            };

            for (int i = 0; i < 169; i++)
            {
                Add(new Sprite(tex)
                {
                    Position = new Vector2(i, i),
                    Size = new Vector2(200)
                });
            }
        }
    }
}