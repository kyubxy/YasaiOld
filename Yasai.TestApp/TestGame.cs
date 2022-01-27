using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Audio;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Box box;
        private Channel channel;
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Root.Add(box = new Box
            {
                Anchor = Anchor.Center,
                Origin = Anchor.Center,
                Size = new Vector2(200)
            });

            AudioStore store = new AudioStore();
            store.LoadResource("eyecatch.mp3", "audio");
            AudioStream audio = store.GetResource("audio");
            channel = new Channel(audio);
            //channel.Play();
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            box.Rotation += 0.01f;
        }
    }
}