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
using Pulsometer.Model.Models;

namespace Pulsometer.Adapters
{
    public class MeasurementsOfDayAdapter : BaseAdapter<Measurement>
    {
        private readonly Context context;
        private readonly IList<Measurement> measurementsOfDay;
        
        public MeasurementsOfDayAdapter(Context context, IList<Measurement> measurementsOfDay)
        {
            this.context = context;
            this.measurementsOfDay = measurementsOfDay;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (position < 0 || position >= Count)
                throw new ArgumentOutOfRangeException(nameof(position));

            //if (position == tripProvider.Count - 2)
            //    tripProvider.FetchCommentsAsync();

            if (convertView == null)
            {
                var inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.Measurement, parent, false);
            }

            var item = measurementsOfDay[position];

            var measurementTime = convertView.FindViewById<TextView>(Resource.Id.measurementTime);
            measurementTime.Text = item.Date.ToShortTimeString();

            var measurementValue = convertView.FindViewById<TextView>(Resource.Id.measurementValue);
            measurementValue.Text = item.Value.ToString("###");

            return convertView;
        }

        public override int Count => measurementsOfDay.Count;

        public override Measurement this[int position]
        {
            get
            {
                if (position < 0 || position >= measurementsOfDay.Count)
                    throw new ArgumentOutOfRangeException(nameof(position));

                return measurementsOfDay[position];
            }
        }
    }
}