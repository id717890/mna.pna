using System;

namespace MNA.Interfaces
{
    public interface IMnaView:IView<IMnaPresenterCallback>
    {
        Int32 ColumnCaption { get; set; }
        Int32 ColumnTag { get; set; }
        Int32 MnaNumber { get; set; }
        Boolean IsMnaNumber { get; set; }

        void SetModel(IMnaViewModel model);
        void RenderParametersGrid();
    }
}