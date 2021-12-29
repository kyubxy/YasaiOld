using System.Collections.Generic;
using Yasai.Graphics;

namespace Yasai.Resources
{
    /// <summary>
    /// Information on how to read a spritesheet
    /// </summary>
    public class SpritesheetData
    {
        /// <summary>
        /// Information about each tile on the spritesheet
        /// </summary>
        public struct Tile
        {
            public Rectangle Rect;

            public Tile(Rectangle r)
            {
                Rect = r;
            }
            
            public Tile (int x, int y, int w, int h) 
                : this (new Rectangle(x, y, w, h))
            { }

            public override string ToString() => $"Spritesheet Tile @[{Rect.X}, {Rect.Y}, {Rect.Width}, {Rect.Height}]";
        }
        
        public Dictionary<string, Tile> SheetData { get; }

        public SpritesheetData()
        {
            SheetData = new Dictionary<string, Tile>();
        }

        public SpritesheetData(Dictionary<string, Tile> data)
        {
            SheetData = data;
        }
    }
}