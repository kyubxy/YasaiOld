namespace Yasai.Structures.Bindables
{
    public class BindableInt : Bindable<int>
    {
        public BindableInt()
        {
            
        }
        
        public BindableInt(int init) : base (init)
        { }
        
        public void Increment()
        {
            RaiseSet(Value++);
        }

        public void Decrement()
        {
            RaiseSet(Value--);
        }

        public override BindableString ToBindableString()
        {
            BindableString ret = new BindableString();
            OnSet += i => ret.Value = i.ToString();
            return ret;
        }
    }
}