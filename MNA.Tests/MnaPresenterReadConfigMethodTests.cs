using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MNA.Interfaces;
using Moq;

namespace MNA.Tests
{
    [TestClass]
    public class MnaPresenterReadConfigMethodTests
    {
        [TestMethod]
        public void MnaViewModelNotNullAfterReadConfig()
        {
            var mnaViewModelMoq = new Mock<IMnaViewModel>();
            var mnaVie = new Mock<IMnaView>();
            var presenterMoq = new Mock<IMnaPresenter>(mnaVie.Object);
            //presenterMoq.Verify(x => x.ReadConfig());
            //presenterMoq.Setup(x => x.ReadConfig());
            //presenter.ReadConfig();
        }
    }
}
