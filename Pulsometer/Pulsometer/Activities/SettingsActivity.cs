using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.Model.Models;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "Ustawienia", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : AppCompatActivity, ISettingsViewAccess
    {
        private readonly LayoutInflater inflater;
        private readonly Context context;
        private readonly SettingsViewModel viewModel;

        private EditText name;
        private EditText age;
        private Spinner gender;
        private Button saveButton;
        private Toolbar toolbar;
        private DrawerLayout mainDrawer;

        public SettingsActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            viewModel = viewModelFactory.GetSettingsViewModel(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);

            InitializeObjects();
            SetSupportActionBar(toolbar);
            SetUpToolbar();
        }

        private void InitializeObjects()
        {
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            name = FindViewById<EditText>(Resource.Id.name);
            gender = FindViewById<Spinner>(Resource.Id.gender);
            age = FindViewById<EditText>(Resource.Id.age);
            saveButton = FindViewById<Button>(Resource.Id.saveButton);

            saveButton.Click += SaveButtonOnClick;
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {

        }

        private void SetUpToolbar()
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }

    }
}