namespace App.Interface.Presenter
{
    public interface IPresenter
    {
        void Initialize();
        object Ui { get; }
    }
}
