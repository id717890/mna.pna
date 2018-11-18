namespace App.Interface.View
{
    public interface IView<TCallbacks>
    {
        void Attach(TCallbacks presenter);
    }
}