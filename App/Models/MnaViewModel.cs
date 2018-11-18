using System.Collections.Generic;
using App.Data;
using App.Interface.Model;

namespace App.Models
{
    public class MnaViewModel:IMnaViewModel
    {
        public Mna CurrentMna { get; set; }
        public IEnumerable<Mna> MnaList { get; set; }
    }
}
