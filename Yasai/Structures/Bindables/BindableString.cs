namespace Yasai.Structures.Bindables
{
    public class BindableString : Bindable<string>
    {
        public virtual void BindToBruh(IBindable<string> master) => BindTo(master.ToBindableString());
    }
}