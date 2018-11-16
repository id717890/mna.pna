using System.Collections.Generic;
using MNA.Data;

namespace MNA.Interfaces
{
    public interface IMnaViewModel
    {
        IEnumerable<Mna> MnaList { get; set; }
        Mna CurrentMna { get; set; }
    }
}