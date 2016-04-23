using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Dialogs
{
    public class WelcomeDialog
    {
        private AlertDialog dialog;
        private readonly MainViewModel viewModel;
        private readonly LayoutInflater inflater;
        private readonly Context context;

        private EditText name;
        private EditText age;
        private Spinner gender;
        private Button saveButton;
        private Button exitButton;

        public WelcomeDialog(Context context, LayoutInflater inflater, MainViewModel viewModel)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
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
            age = dialog.FindViewById<EditText>(Resource.Id.age);
            saveButton = dialog.FindViewById<Button>(Resource.Id.saveButton);
            exitButton = dialog.FindViewById<Button>(Resource.Id.exitButton);

            saveButton.Click += SaveButtonOnClick;
            exitButton.Click += ExitButtonOnClick;
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            viewModel.SetUserConfiguration(name.Text, age.Text, gender.SelectedItem.ToString());
            viewModel.SaveUserConfiguration();
            dialog.Dismiss();
            Toast.MakeText(context, "Zapisano", ToastLength.Short).Show();
        }

        private void ExitButtonOnClick(object sender, EventArgs eventArgs)
        {
            dialog.Dismiss();
            Process.KillProcess(Process.MyPid());
        }
    }
}