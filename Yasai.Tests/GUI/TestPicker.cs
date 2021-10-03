using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Layout;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Layout.Screens;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;
using Yasai.Resources.Loaders;
using Yasai.Tests.Scenarios;

namespace Yasai.Tests.GUI
{
    public sealed class TestPicker : Group
    {
        private int BUTTON_HEIGHT => 40;
        private int BUTTON_WIDTH => 300;
        private int PADDING => 5;
        
        private readonly Type[] _scenarios;
        private readonly ScreenManager _manager;
        private readonly Game _g;

        public override Vector2 Position => new((_g.Window.Width / 2) - (BUTTON_WIDTH / 2),
                    (_g.Window.Height / 2) - (_scenarios.Length * (BUTTON_HEIGHT + PADDING) / 2));
        
        public override Vector2 Size => new(BUTTON_WIDTH, (BUTTON_HEIGHT + PADDING) * _scenarios.Length);

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
            _scenarios = scenarios;
            _manager = sm;
            _g = g;
            Enabled = false;
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache); 
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
            
            for (int i = 0; i < _scenarios.Length; i++)
            {
                Type s = _scenarios[i];
                
                Button b;
                buttons.Add(b = new Button(_manager, s, _g)
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
        private readonly ScreenManager _sm;

        private readonly Scenario _scenario;

        private Box _back;
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
                    _back.Position = position;
                    label.Position = Vector2.Add(Position, new Vector2(10, 10));
                }
            }
        }

        public override bool IgnoreHierarchy => true;

        public Button(ScreenManager sm, Type s, Game game)
        {
            _sm = sm;
            _scenario = (Scenario)Activator.CreateInstance(s, game);

            OnExit  += (_, _) => _back.Colour = Color.White;
            OnEnter += (_, _) => _back.Colour = Color.LightGray;
            OnClick += (_, _) => _back.Colour = Color.Gray;
            OnRelease += (sender, args) =>
            {
                _sm.PushScreen(_scenario);
                OnSelect?.Invoke(sender, args);
            };
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            AddAll(new IDrawable[]
            {
                _back = new Box ()
                {
                    Size = Size,
                    Colour = Color.White
                },
                label = new SpriteText(_scenario.Name, "fnt_smallFont")
                {
                    Colour = Color.Black
                }
            });
        }
    }
}