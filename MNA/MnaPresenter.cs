﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNA
{
    class MnaPresenter
    {
        private IMnaView _view;

        public MnaPresenter(IMnaView view)
        {
            _view = view;
        }
    }
}
