namespace Yasai.Maths
{
    public interface IMatrix
    {
        public double GetAt(int i, int j);
        public void SetAt(double value, int i, int j);
    }
}