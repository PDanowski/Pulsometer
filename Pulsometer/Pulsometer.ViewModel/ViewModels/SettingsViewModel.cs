using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Model.Models;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsViewAccess access;
        private IUserConfiguration config;

        public SettingsViewModel(ISettingsViewAccess access, IUserConfiguration config)
        {
            this.access = access;
            this.config = config;
        }

        public void SetFields()
        {
            access.SetField(config.Name, config.Gender, config.Birthday);
        }

        public void SaveUserConfiguration()
        {
            UserSerializer.Serialize(config);
            access.SetUserConfig(config);
        }

        public void SetUserConfiguration(string name, DateTime birthday, string gender)
        {
            config.Name = name;
            config.Birthday= birthday;
            config.Gender = (Gender)Enum.Parse(typeof(Gender), gender);
        }

    }
}
