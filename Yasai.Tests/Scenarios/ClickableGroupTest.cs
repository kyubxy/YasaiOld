using System;
using OpenTK.Mathematics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Input.Mouse;

namespace Yasai.Tests.Scenarios
{
    public class ClickableGroupTest : Scenario
    {
        ClickableGroup g;

        public ClickableGroupTest()
        {
            Add(g = new ClickableGroup()
            {
                Fill = true,
                Position = new Vector2(400),
                Size = new Vector2(200)
            });
            
        }

        private void GOnOnRelease(object? sender, EventArgs e)
        {
            MouseArgs args = (MouseArgs) e;
        }
    }
}