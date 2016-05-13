using Pulsometer.Model.Models;
using Pulsometer.Model.XMLSerialization;

namespace Pulsometer.ViewModel.Interfaces
{
    public interface IProperPulseRangeCounter
    {
        Range GetAverageRange(IUserConfiguration config);
        Range GetFullRange(IUserConfiguration config);
    }
}