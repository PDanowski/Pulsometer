using Microsoft.Practices.Unity;

namespace Pulsometer.Dependencies
{
    public static class Container
    {
        private static UnityContainer instance;

        private static IUnityContainer GetInstance()
        {
            if (instance == null)
            {
                instance = new UnityContainer();
                Configuration.Configure(instance);
            }

            return instance;
        }

        public static T Resolve<T>()
        {
            return GetInstance().Resolve<T>();
        }
    }
}