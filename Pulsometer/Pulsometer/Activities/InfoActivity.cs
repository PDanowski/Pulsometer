using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Webkit;
using Pulsometer.Dependencies;
using Pulsometer.Services;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "@string/infoItem", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class InfoActivity : AppCompatActivity, IInfoViewAccess
    {
        private readonly InfoViewModel viewModel;
        private SupportToolbar toolbar;
        private DrawerLayout mainDrawer;
        private WebView infoText;

        public InfoActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            viewModel = viewModelFactory.GetInfoViewModel(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Info);

            InitializeObjects();
            SetSupportActionBar(toolbar);
            SetUpToolbar();
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

        private void InitializeObjects()
        {
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mainDrawer = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            infoText = FindViewById<WebView>(Resource.Id.InfoText);

            infoText.LoadDataWithBaseURL(null, viewModel.LoadHtml(Assets.Open("InfoText.html")), "text/html", "utf-8", null);
        }
    }
}