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

        public WelcomeDialog(Context context, LayoutInflater inflater, MainViewModel viewModel)
        {
            this.context = context;
            this.inflater = inflater;
            this.viewModel = viewModel;
        }

        public void Show()
        {
            View dialoglayout = inflater.Inflate(Resource.Layout.MeasuresOfDay, null);
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetView(dialoglayout);

            dialog = builder.Create();
            dialog.Show();

            InitializeObjects();
        }

        private void InitializeObjects()
        {

        }
    }
}