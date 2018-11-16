namespace MNA.Interfaces
{
    public interface IMnaPresenterNew: IPresenter
    {
        void ReadConfig();
        void SetCurrentMna(Data.Mna mna);
        void ResetStatusCurrentMna();
    }
}