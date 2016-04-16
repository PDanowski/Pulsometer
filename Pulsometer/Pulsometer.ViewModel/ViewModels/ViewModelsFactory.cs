using Pulsometer.Model.SQLiteConnection;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        private readonly ISQLiteConnector sqLiteConnector;

        public ViewModelsFactory(ISQLiteConnector sqLiteConnector)
        {
            this.sqLiteConnector = sqLiteConnector;
        }

        public MainViewModel GetMainViewModel(IMainViewAccess access)
        {
            return new MainViewModel(access, sqLiteConnector);
        }

        public CalendarViewModel GetCalendarViewModel(ICalendarViewAccess access)
        {
            return new CalendarViewModel(access, sqLiteConnector);
        }

    }
}