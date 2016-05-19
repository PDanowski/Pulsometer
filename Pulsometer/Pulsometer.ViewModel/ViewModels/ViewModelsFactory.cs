using Pulsometer.Model.SQLiteConnection;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        private readonly IProperPulseRangeCounter properPulseRangeCounter;
        private readonly ISQLiteConnector sqLiteConnector;
        private IUserConfiguration userConfig;

        public ViewModelsFactory(ISQLiteConnector sqLiteConnector, IUserConfiguration config,
            IProperPulseRangeCounter properPulseRangeCounter)
        {
            this.sqLiteConnector = sqLiteConnector;
            userConfig = config;
            this.properPulseRangeCounter = properPulseRangeCounter;
        }

        public void SetUserConfiguration(IUserConfiguration config)
        {
            userConfig = config;
        }

        public MainViewModel GetMainViewModel(IMainViewAccess access)
        {
            return new MainViewModel(access, sqLiteConnector, properPulseRangeCounter);
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

        public LastMeasurementsViewModel GetLastMeasurementsViewModel(ILastMeasurementsViewAccess access)
        {
            return new LastMeasurementsViewModel(access, sqLiteConnector);
        }
    }
}