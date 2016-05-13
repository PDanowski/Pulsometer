using System;
using Pulsometer.Model.Models.Enums;
using Pulsometer.Model.XMLSerialization;

namespace Pulsometer.ViewModel.Interfaces
{
    public interface ISettingsViewAccess
    {
        void SetField(string name, Gender gender, DateTime birthday);
        void SetUserConfig(IUserConfiguration config);
    }
}
