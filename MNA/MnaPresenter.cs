using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNA
{
    public class MnaPresenter: IMnaPresenter
    {
        private IMnaView _view;

        //public MNA View { set => _view = value; }
        public MNA View { get; set; }
    }
}
