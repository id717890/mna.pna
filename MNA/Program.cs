using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MNA.Interfaces;

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

            var kernel = new StandardKernel();

            CompositionRoot.Init(kernel);
            //kernel.Load(Assembly.GetExecutingAssembly());

            CompositionRoot.Wire(new CompositeModule());
            //var kernel = new StandardKernel(new MyInjectModule());

            //Application.Run(CompositionRoot.Resolve<MNA>());
            //Application.Run(CompositionRoot.Resolve<MNA>());
            //Application.Run(CompositionRoot.Resolve<MNA>());
            var presenter = CompositionRoot.Resolve<IMnaPresenterNew>();
            presenter.Initialize();
            Application.Run((Form)presenter.Ui);
        }
    }
}
