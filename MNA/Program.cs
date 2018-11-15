using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CompositionRoot.Wire(new CompositeModule());
            //var kernel = new StandardKernel(new MyInjectModule());

            Application.Run(CompositionRoot.Resolve<MNA>());
            //Application.Run(new MNA());
        }
    }
}
