using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Pulsometer.Model.Models.Enums;
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
        private TextView rangeStatus;
        private Button saveButton;
        private Button cancelButton;
        private EditText noteEditText;
        private ToggleButton stateGeneral;
        private ToggleButton stateBeforeExercise;
        private ToggleButton stateAfterExercise;
        private ToggleButton stateRest;

        private State state;

        public CompletedMeasureDialog(Context context, LayoutInflater inflater, MainViewModel viewModel)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
        }

        private void InitializeObjects()
        {
            heartRate = dialog.FindViewById<TextView>(Resource.Id.heartRate);
            rangeStatus = dialog.FindViewById<TextView>(Resource.Id.rangeStatus);
            noteEditText = dialog.FindViewById<EditText>(Resource.Id.note);
            saveButton = dialog.FindViewById<Button>(Resource.Id.saveButton);
            cancelButton = dialog.FindViewById<Button>(Resource.Id.cancelButton);

            stateGeneral = dialog.FindViewById<ToggleButton>(Resource.Id.generalState);
            stateGeneral.Checked = true;
            stateBeforeExercise = dialog.FindViewById<ToggleButton>(Resource.Id.beforeExerciseState);
            stateAfterExercise = dialog.FindViewById<ToggleButton>(Resource.Id.afterExerciseState);
            stateRest = dialog.FindViewById<ToggleButton>(Resource.Id.restState);

            stateGeneral.Click += (sender, args) => SetState(State.General);
            stateBeforeExercise.Click += (sender, args) => SetState(State.BeforeExercise);
            stateAfterExercise.Click += (sender, args) => SetState(State.AfterExercise);
            stateRest.Click += (sender, args) => SetState(State.Rest);
            saveButton.Click += SaveButtonOnClick;
            cancelButton.Click += CancelButtonOnClick;
        }

        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            dialog.Dismiss();
        }

        private void SetState(State state)
        {
            stateGeneral.Checked = state == State.General;
            stateBeforeExercise.Checked = state == State.BeforeExercise;
            stateAfterExercise.Checked = state == State.AfterExercise;
            stateRest.Checked = state == State.Rest;

            this.state = state;
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            viewModel.SaveMeasurement(noteEditText.Text, this.state);
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
            set
            {
               HandleHeartRate(value);
            }
        }

        private void HandleHeartRate(float heartRateValue)
        {
            heartRate.Text = $"{heartRateValue.ToString("##")} ud./min";
            var averageRange = viewModel.GetAverageRange();
            var fullRange = viewModel.GetFullRange();

            if (averageRange.IsProper(heartRateValue))
            {
                rangeStatus.Text = $"Puls w normie.\nŚredni zakres pulsu spoczynkowego dla Ciebie to: \n{averageRange.Lower} - {averageRange.Upper}";
                rangeStatus.SetTextColor(Color.LimeGreen);
            }
            else
            {
                rangeStatus.Text = $"Puls poza normą !\nŚredni zakres pulsu spoczynkowego dla Ciebie to: \n{averageRange.Lower} - {averageRange.Upper}";
                rangeStatus.SetTextColor(Color.Red);
            }
        }
    }
}