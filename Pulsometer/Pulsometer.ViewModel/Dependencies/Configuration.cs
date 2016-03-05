using Microsoft.Practices.Unity;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.ViewModel.Dependencies
{
    public static class Configuration
    {
        private static bool configured = false;

        public static void Configure(IUnityContainer container)
        {
            if (configured)
                return;

            configured = true;

            container.RegisterType<IViewModelsFactory, ViewModelsFactory>(new ContainerControlledLifetimeManager());
        }
    }
}
