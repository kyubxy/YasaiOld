using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Mouse;
using Yasai.Screens;
using Yasai.VisualTests.Scenarios;

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

        public override void LoadComplete()
        {
            base.LoadComplete(); 
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
                title = new SpriteText("Open a test scenario ..", "fnt_smallFont")
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
            
                b.OnSelect += (sender, args) => Enabled = false;
            }
        }

        public override void MouseMotion(MouseArgs args)
        {
            if (Loaded)
                base.MouseMotion(args);
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
    
    sealed class Button : ClickableGroup
    {
        private readonly ScreenManager sm;

        private readonly Scenario scenario;

        private Box back;
        private SpriteText label;

        public EventHandler OnSelect;

        private Vector2 position;
        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                if (Loaded)
                {
                    back.Position = position;
                    label.Position = Vector2.Add(Position, new Vector2(10, 10));
                }
            }
        }

        public override bool IgnoreHierarchy => true;

        public Button(ScreenManager sm, Type s, Game game)
        {
            this.sm = sm;
            scenario = (Scenario)Activator.CreateInstance(s, game);

            OnExit  += (_, _) => back.Colour = Color.White;
            OnEnter += (_, _) => back.Colour = Color.LightGray;
            OnClick += (_, _) => back.Colour = Color.Gray;
            OnRelease += (sender, args) =>
            {
                this.sm.PushScreen(scenario);
                OnSelect?.Invoke(sender, args);
            };
        }

        public override void LoadComplete()
        {
            base.LoadComplete();
            AddAll(new IDrawable[]
            {
                back = new Box ()
                {
                    Size = Size,
                    Colour = Color.White
                },
                label = new SpriteText(scenario.Name, "fnt_smallFont")
                {
                    Colour = Color.Black
                }
            });
        }
    }
}