using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Model.Models;
using Pulsometer.Model.XMLSerialization;

namespace Pulsometer.ViewModel.Interfaces
{
    public interface ISettingsViewAccess
    {
        void SetField(string name, Gender gender, DateTime birthday);
        void SetUserConfig(IUserConfiguration config);

    }
}
