using System;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Imaging;
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

            dependencies.Register<int>(69);
            Children = new Group(new IDrawable[]
            {
                new Group(new IDrawable[]
                {
                    new Group(new IDrawable[]
                    {
                        new Group(new IDrawable[]
                        {
                            new Group(new IDrawable[]
                            {
                                new Penis()
                            })
                        })
                    })
                })
            });
        }

        public override void Update()
        {
            base.Update();
        }
    }

    class Penis : Drawable
    {
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Console.WriteLine(dependencies.Resolve<int>());
        }
    }
}