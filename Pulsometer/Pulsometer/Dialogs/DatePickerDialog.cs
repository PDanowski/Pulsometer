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

namespace Pulsometer.Dialogs
{
    public class DatePickerDialogFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        private DateTime selectedDate;

        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerDialogFragment NewInstance(Action<DateTime> onDateSelected, DateTime date)
        {
            DatePickerDialogFragment frag = new DatePickerDialogFragment();
            frag._dateSelectedHandler = onDateSelected;
            frag.selectedDate = date;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var dialog = new DatePickerDialog(Activity, this, selectedDate.Year, selectedDate.Month - 1, selectedDate.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            _dateSelectedHandler(selectedDate);
        }
    }
}
