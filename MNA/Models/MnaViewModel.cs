using System.Collections.Generic;
using App.Data;
using App.Interface.Model;

namespace MNA.Models
{
    public class MnaViewModel:IMnaViewModel
    {
        public Mna CurrentMna { get; set; }
        public IEnumerable<Mna> MnaList { get; set; }
    }
}
