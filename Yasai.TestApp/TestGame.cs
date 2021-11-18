using System;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Sprite spr;

        public TestGame()
        {
            var store = new TextureStore(Dependencies);
            store.LoadResource("tex.png");
            Dependencies.Register<TextureStore>(store);
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);

            var store = dependencies.Resolve<FontStore>();

            Children = new Group(new IDrawable[]
            {
                new SpriteText("penis", store.GetResource(SpriteFont.FontTiny))
                {
                    Position = new Vector2(400)
                }
            });
        }
    }
}