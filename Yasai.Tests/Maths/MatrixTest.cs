using System.Numerics;
using Xunit;
using Yasai.Maths;

namespace Yasai.Tests.Maths
{
    public class MatrixTest
    {
        [Fact]
        void testGet()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });

            Assert.Equal(5, a.GetAt(1, 1));
            Assert.Equal(6, a.GetAt(1, 2));
            Assert.Equal(7, a.GetAt(2, 0));
        }
        
        [Fact]
        void testAdd()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });
            
            Matrix3 b = new Matrix3(new double[]
            {
                3, 3, 3,
                3, 3, 3,
                3, 3, 3
            });

            Matrix3 expected = new Matrix3(new double[]
            {
                4,5,6,
                7,8,9,
                10,11,12
            });
            
            Assert.Equal(expected, Matrix.Add(a, b));
        }

        [Fact]
        void testSubtract()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                4,5,6,
                7,8,9,
                10,11,12
            });
            
            Matrix3 b = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });

            Matrix3 expected = new Matrix3(new double[]
            {
                3, 3, 3,
                3, 3, 3,
                3, 3, 3
            });
            
            Assert.Equal(expected, Matrix.Subtract(a, b));
        }

        [Fact]
        void testScalarMultiply()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });

            double scalar = 3;
            
            Matrix3 expected = new Matrix3(new double[]
            {
                3, 6, 9,
                12, 15, 18,
                21, 24, 27 
            });
            
            Assert.Equal(expected, Matrix.ScalarMultiply(scalar, a));
        }
        
        [Fact]
        void testDotMultiplyMatrix()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                4,5,6,
                7,8,9,
                10,11,12
            });
            
            Matrix3 b = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });

            Matrix3 expected = new Matrix3(new double[]
            {
                66,81,96,
                102,126,150,
                138,171,204
            });
            
            Assert.Equal(expected, Matrix.DotMultiply(a, b));
        }
        
        [Fact]
        void testDotMultiplyVector()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            });

            Vector3 vec = new Vector3(1, 2, 3);

            Vector3 expected = new Vector3(14,32,50);
            
            Assert.Equal(expected, Matrix.DotMultiply(a, vec));
        }

        [Fact]
        void testDeterminant()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                6,9,6,
                7,8,9,
                10,11,12
            });

            Assert.Equal(18, Matrix.Determinant(a));
        }

        [Fact]
        void testInverse()
        {
            Matrix3 a = new Matrix3(new double[]
            {
                6,9,6,
                7,8,9,
                10,11,12
            });
            
            Matrix3 expected = new Matrix3(new double[]
            {
                -1/6, -7/3, 11/6,
                1/3, 2/3, -2/3,
                -1/6, 4/3, -5/6
            });
            
            Assert.Equal(expected, Matrix.Inverse(a));
        }
    }
}