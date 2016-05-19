using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using Pulsometer.Dependencies;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "@string/lastMeasurementsItem", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LastMeasurementsActivity : AppCompatActivity, ILastMeasurementsViewAccess
    {
        private readonly LastMeasurementsViewModel viewModel;
        private readonly IViewModelsFactory viewModelFactory;
        private SupportToolbar toolbar;

        private PlotView plot;

        public LastMeasurementsActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            viewModel = viewModelFactory.GetLastMeasurementsViewModel(this);
            
        }

        private void InitializeObjects()
        {
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            plot = FindViewById<PlotView>(Resource.Id.plotView);
            plot.Model = CreatePlotModel();

        }

        private void SetUpToolbar()
        {
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LastMeasurements);

            viewModel.LoadMeasurements();

            InitializeObjects();

            SetUpToolbar();  
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        private PlotModel CreatePlotModel()
        {
            PlotModel model = new PlotModel();
            model.DefaultFontSize = 20;

            var line = new LineSeries();
            line.Color = OxyColor.FromRgb(0, 102, 204);
            line.Smooth = true;
            line.MarkerSize = 6;
            line.MarkerFill = OxyColor.FromRgb(0, 102, 204);
            line.MarkerType = MarkerType.Circle;

            var values = viewModel.GetLastMeasurements();

            int x = 1;
            foreach (float y in values)
            {
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(x)), y));
                x++;
            }

            model.Series.Add(line);

            var ay = new LinearAxis();
            ay.Title = "Wartoœæ pulsu";
            ay.Maximum = 150;
            ay.Minimum = 50;

            model.Axes.Add(ay);

            var ax = new LinearAxis();
            ax.Title = "Pomiary";
            ax.StringFormat = " ";
            ax.MajorGridlineStyle = LineStyle.Solid;
            model.Axes.Add(ax);
            ax.Position = AxisPosition.Bottom;

            return model;
        }
    }
}