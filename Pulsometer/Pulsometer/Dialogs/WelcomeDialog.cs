using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pulsometer.Model.Models.Enums;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Dialogs
{
    public class WelcomeDialog
    {
        private AlertDialog dialog;
        private readonly MainViewModel viewModel;
        private readonly LayoutInflater inflater;
        private readonly Context context;
        private readonly FragmentManager manager;

        private EditText name;
        private Button birthdayButton;
        private Spinner gender;
        private Button saveButton;
        private Button exitButton;
        private TextView birthdayText;
        private DateTime selectedDate;

        public WelcomeDialog(Context context, LayoutInflater inflater, MainViewModel viewModel, FragmentManager manager)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
            this.manager = manager;
        }

        public void Show()
        {
            View dialoglayout = inflater.Inflate(Resource.Layout.Welcome, null);
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetView(dialoglayout);

            dialog = builder.Create();
            dialog.Show();

            InitializeObjects();
        }

        private void InitializeObjects()
        {
            name = dialog.FindViewById<EditText>(Resource.Id.name);
            gender = dialog.FindViewById<Spinner>(Resource.Id.gender);
            birthdayText = dialog.FindViewById<TextView>(Resource.Id.birthdayText);
            birthdayButton = dialog.FindViewById<Button>(Resource.Id.birthdayButton);
            saveButton = dialog.FindViewById<Button>(Resource.Id.saveButton);
            exitButton = dialog.FindViewById<Button>(Resource.Id.exitButton);

            saveButton.Click += SaveButtonOnClick;
            exitButton.Click += ExitButtonOnClick;
            birthdayButton.Click += ShowDatePickerDialog;
        }

        public void ShowDatePickerDialog(object sender, EventArgs eventArgs)
        {
            DateTime defaultDate = DateTime.Today.AddYears(-15);
            DatePickerDialogFragment frag = DatePickerDialogFragment.NewInstance((delegate (DateTime time) 
            {
                birthdayText.Text= time.Date.ToShortDateString();
                selectedDate = time;
            }), defaultDate);
            frag.Show(manager, "Wybierz datê urodzin");

        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            var parsedGender = ParseStringToGenderEnum(gender.SelectedItem.ToString());
            viewModel.SetUserConfiguration(name.Text, selectedDate, parsedGender);
            viewModel.SaveUserConfiguration();
            dialog.Dismiss();
            Toast.MakeText(context, "Zapisano", ToastLength.Short).Show();
        }

        private Gender ParseStringToGenderEnum(string gender)
        {
            var genders = context.Resources.GetStringArray(Resource.Array.genderEnum);
            return gender == genders[0] ? Gender.Man : Gender.Woman;
        }

        private void ExitButtonOnClick(object sender, EventArgs eventArgs)
        {
            dialog.Dismiss();
            Process.KillProcess(Process.MyPid());
        }
    }
}