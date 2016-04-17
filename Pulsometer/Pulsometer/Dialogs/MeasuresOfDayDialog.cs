using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Pulsometer.Adapters;
using Pulsometer.Model.Models;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Dialogs
{
    public class MeasuresOfDayDialog
    {
        private readonly Context context;
        private readonly LayoutInflater inflater;
        private readonly CalendarViewModel viewModel;
        private AlertDialog dialog;

        private TextView day;
        private ListView measurementsOfDay;

        public MeasuresOfDayDialog(Context context, LayoutInflater inflater, CalendarViewModel viewModel)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
        }

        public void Show(IList<Measurement> measurements)
        {
            View dialoglayout = inflater.Inflate(Resource.Layout.MeasuresOfDay, null);
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetView(dialoglayout);

            dialog = builder.Create();
            dialog.Show();

            InitializeObjects(measurements);
        }

        private void InitializeObjects(IList<Measurement> measurements)
        {
            day = dialog.FindViewById<TextView>(Resource.Id.day);
            measurementsOfDay = dialog.FindViewById<ListView>(Resource.Id.measurementsList);

            day.Text = measurements.First().Date.ToShortDateString();
            measurementsOfDay.Adapter = new MeasurementsOfDayAdapter(context, measurements);
        }
    }
}