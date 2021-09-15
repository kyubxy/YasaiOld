using System;
using OpenTK.Mathematics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class MouseTest : Scenario
    {
        public MouseTest()
        {
            Add(new MouseInput(true, true)
            {
                Position = new Vector2(150,100)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(200,200)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(210,200)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(220,200)
            });
        }
    }

    sealed class MouseInput : Group, IMouseListener
    {
        public bool IgnoreHierachy { get; }
        
        private Box box;
        private bool noisy;
        
        public MouseInput (bool ignoreHierachy, bool noisy = false)
        {
            IgnoreHierachy = ignoreHierachy;
            this.noisy = noisy;
        }

        public override void Load(ContentStore cs)
        {
            Add(box = new Box()
            {
                Position = Position,
                Size = new Vector2(200),
                Fill = false
            });
            
            base.Load(cs);
        }

        public void MouseDown(MouseButton button, Vector2 position)
        {
            box.Fill = true;
        }

        public void MouseUp(MouseButton button, Vector2 position)
        {
            box.Fill = false;
        }

        public void MouseMotion(Vector2 position)
        {
            if (noisy)
                Console.WriteLine(position);
        }
    }
}