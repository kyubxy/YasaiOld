using System;
using System.Drawing;
using System.Numerics;
using Yasai.Graphics;
using Yasai.Graphics.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Screens;
using Yasai.Structures.DI;

namespace Yasai.VisualTests.GUI
{
    sealed class Button : Group
    {
        private readonly ScreenManager sm;
        private Scenario scenario;
        private Type scenarioType;

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

        public Button(ScreenManager sm, Type s)
        {
            this.sm = sm;
            scenarioType = s;

            OnExit  += (_, _) => back.Colour = Color.White;
            OnEnter += (_, _) => back.Colour = Color.LightGray;
            OnClick += (_, _) => back.Colour = Color.Gray;
            OnRelease += (sender, args) =>
            {
                scenario = (Scenario)Activator.CreateInstance(s);
                this.sm.PushScreen(scenario);
                OnSelect?.Invoke(sender, args);
            };
        }

        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);

            var fontStore = dependencies.Resolve<FontStore>();
            
            AddAll(new IDrawable[]
            {
                back = new Box ()
                {
                    Size = Size,
                    Colour = Color.White
                },
                label = new SpriteText(scenarioType.Name, fontStore.GetResource(SpriteFont.FontTiny))
                {
                    Colour = Color.Black
                }
            });
        }
    }

}