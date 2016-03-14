using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class MainViewModel
    {
        private readonly IMainViewAccess access;
            
        public MainViewModel(IMainViewAccess access)
        {
            this.access = access;
        }
    }
}