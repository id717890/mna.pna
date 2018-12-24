using System;
using App.Interface.Presenter;
using App.Interface.Model;

namespace App.Interface.View
{
    public interface IMnaView:IView<IMnaPresenterCallback>
    {
        Int32 ColumnCaption { get; set; }
        Int32 ColumnTag { get; set; }
        Int32 MnaNumber { get; set; }
        Boolean IsMnaNumber { get; set; }
        string[] Enginers { set; }
        string Enginer { get; set; }
        string Order { get; set; }

        void SetModel(IMnaViewModel model);
        void RenderParametersGrid();
    }
}