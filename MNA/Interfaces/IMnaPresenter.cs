namespace MNA.Interfaces
{
    public interface IMnaPresenter: IPresenter
    {
        void ReadConfig();
        void SetCurrentMna(Data.Mna mna);
        void ResetStatusCurrentMna();
    }
}