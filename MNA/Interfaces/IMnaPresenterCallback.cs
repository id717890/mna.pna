using MNA.Data;

namespace MNA.Interfaces
{
    public interface IMnaPresenterCallback
    {
        void OnMnaChenged(Mna mna);
        void OnOpenExcelFile(string fileName);
    }
}