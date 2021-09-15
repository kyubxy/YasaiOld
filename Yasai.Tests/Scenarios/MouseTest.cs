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
            Add(new MouseInput(true)
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

    class MouseInput : Group, IMouseListener
    {
        private Box box;
        
        public MouseInput (bool ignoreHierachy)
        {
            IgnoreHierachy = ignoreHierachy;
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

        public bool IgnoreHierachy { get; }
        public void MouseDown(MouseButton button)
        {
            box.Fill = true;
        }

        public void MouseUp(MouseButton button)
        {
            box.Fill = false;
        }
    }
}