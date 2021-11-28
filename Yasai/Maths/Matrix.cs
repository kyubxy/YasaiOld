using System;
using System.Numerics;

namespace Yasai.Maths
{
    /// <summary>
    /// 3x3 matrix helpers
    /// </summary>
    public static class Matrix
    {
        public static Matrix3 Add(Matrix3 left, Matrix3 right)
        {
            Matrix3 ret = Zero;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    ret.SetAt(left.GetAt(i, j) + right.GetAt(i, j), i, j);

            return ret;
        }
        
        public static Matrix3 Subtract(Matrix3 left, Matrix3 right)
            => Add(left, ScalarMultiply(-1, right));

        public static Matrix3 DotMultiply(Matrix3 left, Matrix3 right)
        {
            double a = left.GetAt(0, 0);
            double b = left.GetAt(0, 1);
            double c = left.GetAt(0, 2);
            double d = left.GetAt(1, 0);
            double e = left.GetAt(1, 1);
            double f = left.GetAt(1, 2);
            double g = left.GetAt(2, 0);
            double h = left.GetAt(2, 1);
            double i = left.GetAt(2, 2);

            double j = right.GetAt(0, 0);
            double k = right.GetAt(0, 1);
            double l = right.GetAt(0, 2);
            double m = right.GetAt(1, 0);
            double n = right.GetAt(1, 1);
            double o = right.GetAt(1, 2);
            double p = right.GetAt(2, 0);
            double q = right.GetAt(2, 1);
            double r = right.GetAt(2, 2);

            return new Matrix3(new []
            {
                a*j + b*m + c*p, a*k + b*n + c*q, a*l + b*o + c*r,
                d*j + e*m + f*p, d*k + e*n + f*q, d*l + e*o + f*r,
                g*j + h*m + i*p, g*k + h*n + i*q, g*l + h*o + i*r,
            });
        }

        public static Vector3 DotMultiply(Matrix3 left, Vector3 right)
        {
            double a = left.GetAt(0, 0);
            double b = left.GetAt(0, 1);
            double c = left.GetAt(0, 2);
            double d = left.GetAt(1, 0);
            double e = left.GetAt(1, 1);
            double f = left.GetAt(1, 2);
            double g = left.GetAt(2, 0);
            double h = left.GetAt(2, 1);
            double i = left.GetAt(2, 2);

            double x = right.X;
            double y = right.Y;
            double z = right.Z;

            return new Vector3(
                (float)(a * x + b * y + c * z),
                (float)(d * x + e * y + f * z),
                (float)(g * x + h * y + i * z)
                );
        }

        public static Matrix3 ScalarMultiply(double s, Matrix3 m)
        {
            Matrix3 ret = Zero;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    ret.SetAt(m.GetAt(i,j) * s, i, j);

            return ret;
        }

        public static double Determinant(Matrix3 m)
            => m.GetAt(0, 0) * (m.GetAt(1, 1) * m.GetAt(2, 2) - m.GetAt(1, 2) * m.GetAt(2, 1)) -
               m.GetAt(0, 1) * (m.GetAt(1, 0) * m.GetAt(2, 2) - m.GetAt(1, 2) * m.GetAt(2, 0)) +
               m.GetAt(0, 2) * (m.GetAt(1, 0) * m.GetAt(2, 1) - m.GetAt(1, 1) * m.GetAt(2, 0));

        public static Matrix3 Inverse(Matrix3 m)
        {
            double a = m.GetAt(0, 0);
            double b = m.GetAt(0, 1);
            double c = m.GetAt(0, 2);
            double d = m.GetAt(1, 0);
            double e = m.GetAt(1, 1);
            double f = m.GetAt(1, 2);
            double g = m.GetAt(2, 0);
            double h = m.GetAt(2, 1);
            double i = m.GetAt(2, 2);
            
            return ScalarMultiply(1 / Determinant(m), new Matrix3(new []
            {
                e*i - f*h, c*h - b*i, b*f - c*e,
                f*g - d*i, a*i - c*g, c*d - a*f,
                d*h - e*g, b*g - a*h, a*e - b*d
            }));
        }

        public static Matrix3 Identity => new Matrix3(new double[]
        {
            1, 0, 0,
            0, 1, 0,
            0, 0, 1
        });

        public static Matrix3 Zero => new (new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        #region affine transformation functions
        
        public static Matrix3 GetTranslationMat(Vector2 position)
            => new (new double[] 
            { 
                1, 0, position.X,
                0, 1, position.Y,
                0, 0, 1
            });

        public static Vector2 GetTranslationFromMat(Matrix3 mat)
            => new ((float)mat.GetAt(0, 2), (float)mat.GetAt(1, 2));

        public static Matrix3 GetScaleMat(Vector2 scale)
            => new (new double[] 
            { 
                scale.X, 0, 0,
                0, scale.Y, 0,
                0, 0, 1
            });

        public static Matrix3 GetRotationMat(float angle)
            => new (new [] 
            { 
                Math.Cos(angle), -Math.Sin(angle), 0,
                Math.Sin(angle),  Math.Cos(angle), 0,
                0, 0, 1
            });

        #endregion
    }
}