using App.Data;
using App.Interface.Presenter;

namespace App.Interface.Presenter
{
    public interface IMnaPresenter: IPresenter
    {
        void ReadConfig();
        void SetCurrentMna(Mna mna);
        void ResetStatusCurrentMna();
    }
}