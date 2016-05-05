using Pulsometer.Model.Models;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        private readonly ISQLiteConnector sqLiteConnector;
        private IUserConfiguration userConfig;

        public ViewModelsFactory(ISQLiteConnector sqLiteConnector, IUserConfiguration config)
        {
            this.sqLiteConnector = sqLiteConnector;
            this.userConfig = config;
        }

        public void SetUserConfiguration(IUserConfiguration config)
        {
            this.userConfig = config;
        }

        public MainViewModel GetMainViewModel(IMainViewAccess access)
        {
            return new MainViewModel(access, sqLiteConnector);
        }

        public CalendarViewModel GetCalendarViewModel(ICalendarViewAccess access)
        {
            return new CalendarViewModel(access, sqLiteConnector);
        }

        public InfoViewModel GetInfoViewModel(IInfoViewAccess access)
        {
            return new InfoViewModel(access);
        }

        public SettingsViewModel GetSettingsViewModel(ISettingsViewAccess access)
        {
            return new SettingsViewModel(access, userConfig);
        }
    }
}