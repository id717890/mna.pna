using System.Collections.Generic;
using App.Data;

namespace App.Interface.Model
{
    public interface IMnaViewModel
    {
        IEnumerable<Mna> MnaList { get; set; }
        Mna CurrentMna { get; set; }
    }
}