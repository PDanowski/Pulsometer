using System;
using System.Collections.Generic;
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
using Android.Views;
using Android.Widget;
using Android.Util;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "Informacje", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class InfoActivity : AppCompatActivity
    {
        private SupportToolbar toolbar;
        private DrawerLayout mainDrawer;

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
        }

    }
}