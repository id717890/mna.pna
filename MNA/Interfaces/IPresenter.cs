namespace MNA.Interfaces
{
    public interface IPresenter
    {
        void Initialize();
        object Ui { get; }
    }
}