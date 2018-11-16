using System;

namespace MNA.Interfaces
{
    public interface IMnaViewNew:IView<IMnaPresenterCallback>
    {
        Int32 ColumnCaption { get; set; }
        Int32 ColumnTag { get; set; }

        void SetModel(IMnaViewModel model);
        void RenderParametersGrid();
    }
}