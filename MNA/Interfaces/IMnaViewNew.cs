using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MNA.Data;
using MNA.Models;

namespace MNA.Interfaces
{
    public interface IMnaViewNew:IView<IMnaPresenterCallbacks>
    {
        Int32 ColumnCaption { get; set; }
        Int32 ColumnTag { get; set; }

        //string MyText { get; set; }
        string SaveButtonText { get; set; }
        bool SaveButtonEnabled { get; set; }

        ListBox MnaListBox { get; set; }

        void SetModel(IMnaViewModel model);
        void RenderParametersGrid();
    }
}