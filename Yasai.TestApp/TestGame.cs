using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Containers;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shapes;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Container c;
        private Drawable b;

        private Sprite sp;
        
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
                        b = new Sprite(t)
                        {
                            Anchor = Anchor.Center, 
                            Origin = Anchor.Center, 
                            Size = new Vector2(100),
                        }
                    }
                }
            };

            sp = new Sprite(t)
            {
                Anchor = Anchor.Center,
                Origin = Anchor.Center,
                Size = new Vector2(100),
            };
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            sp.Load(dependencies);
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            //b.Rotation += 0.01f;
        }

        protected override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            drawPrimitive(sp);
        }
        
        private void drawPrimitive(Primitive primitive)
        {
           //if (!primitive.Enabled || !primitive.Visible)
           //    return;
            
            var shader = primitive.Shader;
            
            primitive.Draw();
            shader.Use();
            
            // assuming the drawable uses a vertex shader with model and projection matrices
            shader.SetMatrix4("model", primitive.ModelTransforms);
            shader.SetMatrix4("projection", GameBase.Projection);
            
            GL.DrawElements(PrimitiveType.Triangles, primitive.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}