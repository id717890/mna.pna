using Ninject.Modules;
using App.Interface.Model;
using App.Interface.View;
using App.Models;
using App.Interface.Presenter;

namespace App
{
    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IMnaPresenter>().To<MnaPresenterNew>();
            Bind<IMnaView>().To<MNA>();
            Bind<IMnaViewModel>().To<MnaViewModel>();

            //Bind<IMnaPresenter>().To<MnaPresenter>();
            //Bind<IMnaView>().To<MNA>();
            //Bind(typeof(IMnaView)).To(typeof(MNA));
            //Bind(typeof(IMnaPresenter)).To(typeof(MnaPresenter));
            //Bind<MNA>().ToSelf();
        }
    }
}
