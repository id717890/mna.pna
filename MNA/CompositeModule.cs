using Ninject.Modules;
using MNA.Interfaces;
using MNA.Models;

namespace MNA
{
    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IMnaPresenter>().To<MnaPresenterNew>();
            Bind<IMnaViewNew>().To<MNA>();
            Bind<IMnaViewModel>().To<MnaViewModel>();

            //Bind<IMnaPresenter>().To<MnaPresenter>();
            //Bind<IMnaView>().To<MNA>();
            //Bind(typeof(IMnaView)).To(typeof(MNA));
            //Bind(typeof(IMnaPresenter)).To(typeof(MnaPresenter));
            //Bind<MNA>().ToSelf();
        }
    }
}
