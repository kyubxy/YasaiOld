using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Layout;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Input.Keyboard;
using Yasai.Resources;
using Yasai.Resources.Loaders;
using Yasai.Tests.Scenarios;

namespace Yasai.Tests.GUI
{
    public class TestPicker : Group
    {
        private readonly Type[] _scenarios;
        private readonly ScreenManager _manager;
        private readonly Game _g;

        private int BUTTON_HEIGHT => 40;
        private int BUTTON_WIDTH => 300;
        private int PADDING => 5;

        private int screenwidth = 1366;
        private int screenheight = 768;

        public override bool Enabled { get; set; } = false;

        public sealed override Vector2 Position => new((screenwidth / 2) - (BUTTON_WIDTH / 2),
            (screenheight / 2) - (_scenarios.Length * (BUTTON_HEIGHT + PADDING) / 2));

        public sealed override Vector2 Size =>
            new(BUTTON_WIDTH, (BUTTON_HEIGHT + PADDING) * _scenarios.Length);

        public TestPicker(Game g, ScreenManager sm, Type[] scenarios)
        {
            _scenarios = scenarios;
            _manager = sm;
            _g = g;

            AddAll(new IDrawable[]
            {
                new Box ()
                {
                    Position = Vector2.Subtract(Position, new Vector2(PADDING)),
                    Size = Vector2.Add (Size, new Vector2(PADDING * 2)),
                    Colour = Color.Aqua
                },
                new Box ()
                {
                    Position = Vector2.Subtract(Position, new Vector2(PADDING, PADDING + 30)),
                    Size = new Vector2(Size.X + PADDING * 2, 30),
                    Colour = Color.Tomato
                },
                new SpriteText("Open a test scenario ..", "_testpickerfont")
                {
                    Position = Vector2.Subtract(Position, new Vector2(0, 30)),
                    Colour = Color.White
                }
            });

            for (int i = 0; i < _scenarios.Length; i++)
            {
                Type s = _scenarios[i];
                
                Button b;
                Add(b = new Button(_manager, s)
                {
                    Position = Vector2.Add (Position,new Vector2(0,  + i * (BUTTON_HEIGHT + PADDING))),
                    Size = new Vector2(BUTTON_WIDTH, BUTTON_HEIGHT),
                });
 
                b.OnSelect += (sender, args) => Enabled = false;               
            }
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache); 
            cache.LoadResource("tahoma.ttf","_testpickerfont", new FontArgs(15));
        }

        public override void KeyDown(KeyCode key)
        {
            base.KeyDown(key);
            if (key == KeyCode.LSHIFT)
                Enabled = !Enabled;
        }
    }

    sealed class Button : ClickableGroup
    {
        private readonly ScreenManager _sm;

        private readonly Scenario _scenario;

        private Box _back;

        public EventHandler OnSelect;
        
        public Button(ScreenManager sm, Type s)
        {
            _sm = sm;
            _scenario = (Scenario) Activator.CreateInstance(s);
            
            IgnoreHierachy = true;

            OnExit += (sender, args) => _back.Colour = Color.White;
            OnEnter += (sender, args) => _back.Colour = Color.LightGray;
            OnClick += (sender, args) => _back.Colour = Color.Gray;
            OnRelease += OnClickFunc;
        }

        public override void Start(ContentCache cache)
        {
            base.Start(cache);
            AddAll(new IDrawable[]
            {
                _back = new Box ()
                {
                    Position = Position,
                    Size = Size,
                    Colour = Color.White
                },
                new SpriteText(_scenario.Name, "_testpickerfont")
                {
                    Position = Vector2.Add (Position, new Vector2(10, 10)),
                    Colour = Color.Black
                }
            });
        }

        void OnClickFunc(object sender, EventArgs args)
        {
            _sm.PushScreen(_scenario);
            OnSelect?.Invoke(sender, args);
        }
    }
}