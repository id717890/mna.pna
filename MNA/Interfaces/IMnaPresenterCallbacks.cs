using MNA.Data;
using System;

namespace MNA.Interfaces
{
    public interface IMnaPresenterCallbacks
    {
        void OnSave();
        void OnMyTextChanged();
        void OnMnaChenged(Mna mna);
        void OnOpenExcelFile(string fileName);
    }
}