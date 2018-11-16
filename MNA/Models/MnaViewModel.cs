using System.Collections.Generic;
using MNA.Data;
using MNA.Interfaces;

namespace MNA.Models
{
    public class MnaViewModel:IMnaViewModel
    {
        public Mna CurrentMna { get; set; }
        public IEnumerable<Mna> MnaList { get; set; }
    }
}
