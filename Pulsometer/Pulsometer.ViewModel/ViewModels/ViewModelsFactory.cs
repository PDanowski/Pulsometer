using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        public ViewModelsFactory()
        {

        }

        public MainViewModel GetMainViewModel(IMainViewAccess access)
        {
            return new MainViewModel(access);
        }

    }
}