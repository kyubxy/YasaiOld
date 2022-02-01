using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Box child;
        private Box parent;
        private float rot;
        private Vector2 pos;

    public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Root.Add(parent = new Box
            {
                Size = new Vector2(200),
                Colour = Color.MediumSpringGreen,
                Origin = Anchor.TopLeft,
                Position = new Vector2(300)
            });
            Root.Add(child = new Box
            {
                Colour = Color.Red,
                Position = new Vector2(1),
                Origin = Anchor.Right,
                Size = new Vector2(200),
            });
        }

        private float s;
        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            s += 0.01f;

            child.Rotation = s;
            child.X += 2f;
            
            child.p =
                Matrix4.CreateTranslation(1,1,0) * 
                Matrix4.CreateRotationZ(s) *
                Matrix4.CreateTranslation(300,300,0) *
                Matrix4.Identity;

            parent.Rotation = s;
        }
    }
}