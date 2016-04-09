using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Dialogs
{
    public class CompletedMeasureDialog
    {
        private readonly Context context;
        private readonly LayoutInflater inflater;
        private readonly MainViewModel viewModel;
        private AlertDialog dialog;

        private TextView heartRate;
        private Button saveButton;
        private EditText noteEditText;

        public CompletedMeasureDialog(Context context, LayoutInflater inflater, MainViewModel viewModel)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
        }

        private void InitializeObjects()
        {
            heartRate = dialog.FindViewById<TextView>(Resource.Id.heartRate);
            noteEditText = dialog.FindViewById<EditText>(Resource.Id.note);
            saveButton = dialog.FindViewById<Button>(Resource.Id.saveButton);
            saveButton.Click += SaveButtonOnClick;
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            viewModel.SaveMeasurement(noteEditText.Text);
            dialog.Dismiss();
        }

        public void Show()
        {
            View dialoglayout = inflater.Inflate(Resource.Layout.MeasureCompleted, null);
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetView(dialoglayout);

            dialog = builder.Create();
            dialog.Show();

            InitializeObjects();
        }

        public float HeartRate
        {
            set { heartRate.Text = value.ToString("##"); }
        }
    }
}