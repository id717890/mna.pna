using App.Data;

namespace App.Interface.Presenter
{
    public interface IMnaPresenter: IPresenter
    {
        void InitViewForm();
        void ReadConfig();
        void SetCurrentMna(Mna mna);
        void ResetStatusCurrentMna();
        void OnCreateProtocolStation();
        void OnCreateProtocolMnaPna();
    }
}