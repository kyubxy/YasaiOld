using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shapes;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Container c;
        private Drawable b;
        
        public TestGame()
        {
            TextureStore tstore = new TextureStore(Dependencies);
            tstore.LoadResource("tex.png");
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
                        b = new Sprite(t)
                        {
                            Anchor = Anchor.Center, 
                            Origin = Anchor.Center, 
                            Size = new Vector2(100),
                            Colour = Color.Blue,
                        }
                    }
                }
            };
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            b.Rotation += 0.01f;
        }
    }
}