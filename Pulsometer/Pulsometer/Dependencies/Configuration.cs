using Microsoft.Practices.Unity;
using Pulsometer.Services;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.Dependencies
{
    public static class Configuration
    {
        private static bool isConfigured = false;

        public static void Configure(IUnityContainer container)
        {
            if (isConfigured)
                return;

            // Configuring dependent assemblies
            Pulsometer.ViewModel.Dependencies.Configuration.Configure(container);

            // Configuring local types

            isConfigured = true;
        }
    }
}