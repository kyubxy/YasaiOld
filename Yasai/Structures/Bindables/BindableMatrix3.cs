using System;
using Yasai.Maths;

namespace Yasai.Structures.Bindables
{
    public class BindableMatrix3 : Bindable<Matrix3>, IMatrix
    {
        public BindableMatrix3(Matrix3 matrix) : base (matrix)
        { }

        public BindableMatrix3(double[] args) : this(new Matrix3(args)) { }
        
        public BindableMatrix3() { }
        
        public double GetAt(int i, int j)
        {
            RaiseGet();
            return Value.GetAt(i, j);
        }

        public void SetAt(double value, int i, int j)
        {
            Value.SetAt(value, i, j);
            RaiseChanged(Value);
        }
    }
}