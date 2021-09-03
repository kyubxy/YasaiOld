namespace Yasai.Graphics.Imaging
{
    public class Texture : IResource
    {
        public bool Loaded { get; protected set; }

        // TODO: load the SDL texture
        public void Load()
        {
            Loaded = true;
            throw new System.NotImplementedException();
        }

        // TODO: dispose of the SDL texture
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}