using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Mouse;
using Yasai.Resources.Stores;
using Yasai.Screens;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.GUI
{
    public sealed class TestPicker : Group
    {
        private int BUTTON_HEIGHT => 40;
        private int BUTTON_WIDTH => 300;
        private int PADDING => 5;
        
        private readonly Type[] scenarios;
        private readonly ScreenManager manager;
        private readonly Game g;

        public override Vector2 Position => new((g.Window.Width / 2) - (BUTTON_WIDTH / 2),
                    (g.Window.Height / 2) - (scenarios.Length * (BUTTON_HEIGHT + PADDING) / 2));
        
        public override Vector2 Size => new(BUTTON_WIDTH, (BUTTON_HEIGHT + PADDING) * scenarios.Length);

        private Box headerBox;
        private Box bodyBox;
        private SpriteText title;
        private Group buttons;

        public override bool IgnoreHierarchy => false;

        private bool enabled;
        public override bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                updatePositions();
            }
        }

        public TestPicker(Game g, ScreenManager sm, Type[] scenarios)
        {
            this.scenarios = scenarios;
            manager = sm;
            this.g = g;
            Enabled = false;
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);

            var fontStore = dependencies.Resolve<FontStore>();
            
            AddAll(new IDrawable[]
            {
                bodyBox = new Box ()
                {
                    Size = Vector2.Add (Size, new Vector2(PADDING * 2)),
                    Colour = Color.Aqua
                },
               headerBox = new Box ()
               {
                   Size = new Vector2(Size.X + PADDING * 2, 30),
                   Colour = Color.Tomato
               },
               title = new SpriteText("Open a test scenario ..", fontStore.GetResource(SpriteFont.FontTiny))
               {
                   Colour = Color.White
               },
               buttons = new Group()
               {
                   IgnoreHierarchy = true
               }
            });
            
            foreach (var s in scenarios)
            {
                Button b;
                buttons.Add(b = new Button(manager, s, g)
                {
                    Size = new Vector2(BUTTON_WIDTH, BUTTON_HEIGHT),
                });
            
                b.OnSelect += (_, _) => Enabled = false;
            }
        }

        void updatePositions()
        {
            if (Loaded)
            {
                int i = 0;
                foreach (var drawable in buttons)
                {
                    var b = (ISimpleGeometry)drawable;
                    b.Position = Vector2.Add(Position, new Vector2(0, +i * (BUTTON_HEIGHT + PADDING)));
                    i++;
                }

                headerBox.Position = Vector2.Subtract(Position, new Vector2(PADDING, PADDING + 30));
                bodyBox.Position = Vector2.Subtract(Position, new Vector2(PADDING));
                title.Position = Vector2.Subtract(Position, new Vector2(0, 30));
            }
        }
    }
}