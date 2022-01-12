using System;
using OpenTK.Mathematics;

namespace Yasai.Graphics.Primitives
{
    public interface ILine
    {
        Vector2 Point1 { get; set; }
        Vector2 Point2 { get; set; }

        float Outline { get; set; }
    }

    public class PLine : Quad, ILine
    {
        public Vector2 Point1 { get; set; }
        public Vector2 Point2 { get; set; }
        public float Outline { get; set; }

        private Vector2 pOffset 
        {
            get
            {
                float angle(Vector2 a, Vector2 b) => (float) Math.Asin((b.Y - a.Y) / (b.X - a.X));
                float theta = angle(Point1, Point2);
                float halfT = Outline / 2;
                return halfT * new Vector2((float)Math.Sin(theta), (float)Math.Cos(theta));
            }
        }

        protected sealed override Vector2 TopRightVertex    => new(Point2.X - pOffset.X, Point2.Y - pOffset.Y);
        protected sealed override Vector2 TopLeftVertex     => new(Point1.X - pOffset.X, Point1.Y - pOffset.Y);
        protected sealed override Vector2 BottomRightVertex => new(Point2.X + pOffset.X, Point2.Y + pOffset.Y);
        protected sealed override Vector2 BottomLeftVertex  => new(Point1.X + pOffset.X, Point1.Y + pOffset.Y);
    }
}