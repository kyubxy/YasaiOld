using System;
using System.Numerics;
using System.Text;

namespace Yasai.Maths
{
    // until we find another maths library with matrices
    // we'll tightly couple to this one instead
    
    /// <summary>
    /// 3x3 double matrix
    /// </summary>
    public struct Matrix3 : IMatrix
    {
        double[] internals;

        // oh
        public const double TOLERANCE = 1d;
        
        public Matrix3(double[] internals)
        {
            if (internals.Length != 9)
                throw new InvalidOperationException("malformed matrix, ensure there are 9 entries in the array");

            this.internals = internals;
        }

        public double GetAt(int i, int j)
        {
            var pos = i * 3 + j;
            if (pos > internals.Length)
                throw new IndexOutOfRangeException($"{i}, {j} is not a coordinate in the matrix");
            return internals[pos];
        }

        public void SetAt(double value, int i, int j)
        {
            var pos = i * 3 + j;
            if (pos > internals.Length)
                throw new IndexOutOfRangeException($"{i}, {j} is not a coordinate in the matrix");
            internals[pos] = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\n[");

            for (int i = 0; i < internals.Length; i++)
            {
                if (i != 0)
                {
                    if (i % 3 == 0)
                        sb.Append("]\n[");
                    else
                        sb.Append(", ");
                }

                sb.Append(internals[i]);
            }

            sb.Append("]");
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Matrix3 other)
                return false;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    var a = GetAt(i, j);
                    var b = other.GetAt(i, j);
                    if (Math.Abs(a - b) > TOLERANCE)
                        return false;
                }

            return true;
        }
        
        public static Matrix3 operator *(Matrix3 a, Matrix3 b) => Matrix.DotMultiply(a, b);
    }
}