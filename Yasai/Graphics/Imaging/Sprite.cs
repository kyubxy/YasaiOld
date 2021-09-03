using System.Collections.Generic;

namespace Yasai.Graphics.Imaging
{
    public class Sprite : ISpriteBase
    {
        public bool Enabled { get; set; }
        public bool Loaded { get; }
        public bool Visible { get; set; }

        // TODO: animation support
        public Sprite CurrentTexture;
        public List<Sprite> Costumes;
        
        public void Load()
        {
            throw new System.NotImplementedException();
        }
        
        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void NextCostume()
        {
            
        }

        public void ToCostume(int n)
        {
            
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}