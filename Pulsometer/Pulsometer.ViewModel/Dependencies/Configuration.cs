using Microsoft.Practices.Unity;
using Pulsometer.Model.Models;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.Model.XMLSerialization;
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
            container.RegisterType<ISQLiteConnector, SQLiteConnector>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserConfiguration, UserConfiguration>(new ContainerControlledLifetimeManager());
        }
    }
}
