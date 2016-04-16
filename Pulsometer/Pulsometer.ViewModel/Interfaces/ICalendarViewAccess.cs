using System;
using System.Collections.Generic;
using Pulsometer.Model.Models;

namespace Pulsometer.ViewModel.Interfaces
{
    public interface ICalendarViewAccess
    {
        void SetMinDate(DateTime minDate);
        void SetCalendarModels(List<Measurement> measurements);
        void OpenWindowWithSelectedMeasurements(List<Measurement> measurements);
    }
}