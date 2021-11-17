using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Imaging;
using Yasai.Resources;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Texture tex;
        private Sprite spr;

        public override void Load(ContentStore store)
        {
            base.Load(store);
            store.LoadResource("tex.png");
            tex = store.GetResource<Texture>("tex");
        }

        public override void LoadComplete()
        {
            base.LoadComplete();
            Children = new Group(new IDrawable[]
            {
                spr = new Sprite(tex)
                {
                    Position = new Vector2(300),
                    Size = new Vector2(140),
                }
            });
        }

        public override void Update()
        {
            base.Update();
            spr.Rotation += 1f;
        }
    }
}