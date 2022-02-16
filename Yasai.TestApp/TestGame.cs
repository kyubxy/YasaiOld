using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Shapes;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Box box;
        private Line line;
        private Wang2 w;

        class Wangs : Quad
        {
            protected override Vector2 TopRightVertex => new (1,0);
            protected override Vector2 TopLeftVertex => new(0, 1);
            protected override Vector2 BottomRightVertex => new (0.5f, 0.5f);
            protected override Vector2 BottomLeftVertex => new (-1, -1);
        }

        class Wang2 : Wangs
        {
            public override void Load(DependencyContainer container)
            {
                base.Load(container);
                var shaderStore = container.Resolve<ShaderStore>();

                Shader = shaderStore.GetResource("yasai_solidShader");
                Shader.Use();
                
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
                
                Loaded = true;
            }

            public override void Draw()
            {
                base.Draw();
                GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
                Shader.SetVector3("colour", new Vector3(Colour.R/(float)255, Colour.G/(float)255, Colour.B/(float)255));
                Shader.SetFloat("alpha", Alpha);
            }
        }

        public TestGame()
        {
        }
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            
            Root.Add(box = new Box
            {
                Anchor = Anchor.Center,
                Origin = Anchor.Center,
                Size = new Vector2(200),
                Colour = Color.Red
            });

            Root.Add(line = new Line
            {
                Point1 = new Vector2(100),
                Point2 = new Vector2(400),
                Outline = 30,
            });
        }

        private float i = 0;
        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            i += 0.01f;
            line.Point2 += new Vector2(i, 0);
        }
    }
}