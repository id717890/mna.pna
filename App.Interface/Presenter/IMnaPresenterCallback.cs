using App.Data;

namespace App.Interface.Presenter
{
    public interface IMnaPresenterCallback
    {
        void OnMnaChenged(Mna mna);
        void OnOpenExcelFile(string fileName);
    }
}