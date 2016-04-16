using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.ViewModel.Interfaces
{
    public interface IViewModelsFactory
    {
        MainViewModel GetMainViewModel(IMainViewAccess access);
        CalendarViewModel GetCalendarViewModel(ICalendarViewAccess access);
    }
}
