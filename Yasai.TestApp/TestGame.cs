using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Container c;
        private Drawable b;

        sealed class TestSprite : Sprite
        {
            public TestSprite(Texture texture) : base(texture)
            {
                Anchor = Anchor.Center;
                Origin = Anchor.Center;
                Size = new Vector2(100);
            }

            public override bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
            {
                Console.WriteLine("some of this wangs");
                return true;
            }
        }
        
        public TestGame()
        {
            TextureStore tstore = new TextureStore(Dependencies);
            tstore.LoadResource("school.png", "tex");
            Texture t = tstore.GetResource("tex");
            
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
                        b = new TestSprite(t)
                    }
                }
            };

            c.MousePressEvent += (_, _) => Console.WriteLine("wangs");
        }
    }
}