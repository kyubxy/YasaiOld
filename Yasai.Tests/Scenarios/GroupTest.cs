using OpenTK.Mathematics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Layout;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class GroupTest : Scenario
    {
        public GroupTest()
        {
            //SingleGroup();
            MultipleGroups();
        }

        public void SingleGroup()
        {
            Group g = new Group();
            g.Add(new Sprite ("ino")
            {
                Size = new Vector2(400)
            });
            Add(g);
        }

        public void MultipleGroups()
        {
            Group a = new Group();
            Group b = new Group();
            Group c = new Group();
            Group d = new Group();
            
            d.Add(new Sprite ("ino"));
            c.Add(d);
            b.Add(c);
            a.Add(b);

            Add(a);
        }

        public override void Load(ContentStore cs)
        {
            cs.LoadResource("ino.png");
            base.Load(cs);
        }
    }
}