using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
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
        }

        public override void Load(DependencyContainer dependencies)
        {
            FontStore fonts = new FontStore();
            fonts.LoadResource("font.fnt", SpriteFont.Normal);
            dependencies.Register<FontStore>(fonts);
            
            base.Load(dependencies);
        }
    }

    class Screen1 : Screen
    {
        public override void Load(DependencyContainer container)
        {
            var fonts = container.Resolve<FontStore>();

            Items = new IDrawable[]
            {
                new SpriteText("screen 1", fonts.GetResource(SpriteFont.Normal))
                {
                    Anchor = Anchor.Center 
                }
            };
            
            base.Load(container);
        }
    }

    class Screen2
    {
        
    }
}