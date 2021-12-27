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
                        b = new TestSprite(t, "left")
                        {
                            Anchor = Anchor.Center,
                            Origin = Anchor.Right,
                            X = 30
                        },
                        new TestSprite(t, "right")
                        {
                            Anchor = Anchor.Center,
                            Origin = Anchor.Left,
                            X = -30
                        }
                    }
                }
            };

            c.MousePressEvent += (_, _) => Console.WriteLine("wangs");
        }
    }
}