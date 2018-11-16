using MNA.Data;

namespace MNA.Interfaces
{
    public interface IMnaPresenterCallback
    {
        void OnSave();
        void OnMyTextChanged();
        void OnMnaChenged(Mna mna);
        void OnOpenExcelFile(string fileName);
    }
}