namespace Yasai.Structures.Bindables
{
    public class BindableString : Bindable<string>
    {
        public virtual void BindTo(BindableInt master) => BindTo(master.ToBindableString());
    }
}