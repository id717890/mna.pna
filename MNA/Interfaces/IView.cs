namespace MNA.Interfaces
{
    public interface IView<TCallbacks>
    {
        void Attach(TCallbacks presenter);
    }
}