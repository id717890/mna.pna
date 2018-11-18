using Ninject;
using System;
using System.Windows.Forms;
using App.Interface.Presenter;

namespace App
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
            var presenter = CompositionRoot.Resolve<IMnaPresenter>();
            presenter.Initialize();
            Application.Run((Form)presenter.Ui);
        }
    }
}
