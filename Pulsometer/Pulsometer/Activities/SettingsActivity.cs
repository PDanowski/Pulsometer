using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.Dialogs;
using Pulsometer.Model.Models.Enums;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "Ustawienia", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : AppCompatActivity, ISettingsViewAccess
    {
        private readonly SettingsViewModel viewModel;
        private readonly IViewModelsFactory viewModelFactory;

        private EditText name;
        private TextView birthday;
        private Spinner gender;
        private Button saveButton;
        private Button birthdayButton;
        private Toolbar toolbar;

        private DateTime selectedDate;

        public SettingsActivity()
        {
            viewModelFactory = Container.Resolve<IViewModelsFactory>();
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
            birthday = FindViewById<TextView>(Resource.Id.birthday);
            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            birthdayButton = FindViewById<Button>(Resource.Id.birthdayButton);

            saveButton.Click += SaveButtonOnClick;
            birthdayButton.Click += ShowDatePickerDialog;

            viewModel.SetFields();
        }

        public void SetField(string uname, Gender ugender, DateTime ubirthday)
        {
            name.Text = uname;
            birthday.Text = ubirthday.Date.ToShortDateString();
            selectedDate = ubirthday;
            gender.SetSelection((int)ugender);
        }

        public void ShowDatePickerDialog(object sender, EventArgs eventArgs)
        {
            DatePickerDialogFragment frag = DatePickerDialogFragment.NewInstance((delegate (DateTime time)
            {
                birthday.Text = time.Date.ToShortDateString();
                selectedDate = time;
            }), selectedDate);
            frag.Show(FragmentManager, "Wybierz datê urodzin");
        }

        public void SetUserConfig(IUserConfiguration config)
        {
            viewModelFactory.SetUserConfiguration(config);
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            var parsedGender = ParseStringToGenderEnum(gender.SelectedItem.ToString());
            viewModel.SetUserConfiguration(name.Text, selectedDate, parsedGender);
            viewModel.SaveUserConfiguration();
            Toast.MakeText(this, "Zapisano", ToastLength.Short).Show();
        }
        
        private Gender ParseStringToGenderEnum(string gender)
        {
            var genders = Resources.GetStringArray(Resource.Array.genderEnum);
            return gender == genders[0] ? Gender.Man : Gender.Woman;
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