using System.Drawing;
using Yasai.Graphics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            TextureStore texStore = new TextureStore();
            texStore.LoadResource("bruh.png");
            texStore.LoadResource(@"Fonts/segoe_0.png", "school");

            Texture bruh = texStore.GetResource("bruh");
            Texture school = texStore.GetResource("school");

            FontStore fonts = Dependencies.Resolve<FontStore>();
            
            Root.AddAll(new IDrawable[]
            {
                new SpriteText("big wangs",  fonts.GetResource(SpriteFont.Segoe))
                {
                    Anchor = Anchor.Right,
                    Origin = Anchor.Right,
                    Colour = Color.Navy,
                    TextAlign = Align.Right
                },
            });
        }
    }
}