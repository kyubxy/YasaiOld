using System;

namespace Yasai.Graphics.Primitives
{
    // software accelerated circle
    // TODO: do this via shader
    public class PCircle : Primitive
    {
        public int Resolution;

        protected override float[] Vertices
        {
            get { throw new NotImplementedException(); }
        }

        public override uint[] Indices { get; }
    }
}