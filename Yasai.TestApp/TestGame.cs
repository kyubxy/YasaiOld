using OpenTK.Mathematics;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Root.AddAll(new IDrawable[]
            {
                new Line()
                {
                    Point1 = new Vector2(1),
                    Point2 = new Vector2(-5),
                    Outline = 0.1f,
                    Size = new Vector2(500)
                }
            });
        }
    }
}