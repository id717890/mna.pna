using Ninject;
using Ninject.Modules;

namespace MNA
{
    class CompositionRoot
    {
        private static IKernel _ninjectKernel;

        public static void Wire(INinjectModule module)
        {
            _ninjectKernel = new StandardKernel(module);
        }

        public static void Init(IKernel kernel)
        {
            _ninjectKernel = kernel;
        }

        public static T Resolve<T>()
        {
            return _ninjectKernel.Get<T>();
        }
    }
}
