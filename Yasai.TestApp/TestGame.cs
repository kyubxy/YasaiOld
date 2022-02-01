using System;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics.Shapes;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Box box;
        private Box box2;
        private float rot;
        private Vector2 pos;

    public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Root.Add(box2 = new Box
            {
                Size = new Vector2(200),
                Colour = Color.MediumSpringGreen
            });
            Root.Add(box = new Box
            {
                Size = new Vector2(200),
                Colour = Color.Red
            });
        }

        private float s;
        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            s += 0.01f;

            box.Position += new Vector2(0.01f);
            box.Rotation -= 0.01f;
            
            box.p =
                Matrix4.CreateTranslation(1,-1,0) * 
                Matrix4.CreateRotationZ(s) *
                Matrix4.CreateScale(new Vector3(80)) *
                Matrix4.CreateTranslation(300,300,0) *
                Matrix4.Identity;
            
            box2.p =
                Matrix4.CreateTranslation(1,-1,0) * 
                Matrix4.CreateRotationZ(s) *
                Matrix4.CreateScale(new Vector3(100)) *
                Matrix4.CreateTranslation(300,300,0) *
                Matrix4.Identity;
        }
    }
}