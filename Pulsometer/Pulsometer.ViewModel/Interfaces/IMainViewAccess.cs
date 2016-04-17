namespace Pulsometer.ViewModel.Interfaces
{
    public interface IMainViewAccess
    {
        //void StartHRMSensor();
        void DisplayCompletedMeasureDialog(float measureValue);
        void RegisterHRMSensore();
        void UnregisterHRMSensore();
        void DisplayProgressDialog();
        void CloseProgressDialog();
        void DisplaySuccessfullSavedDataMessage();
    }
}